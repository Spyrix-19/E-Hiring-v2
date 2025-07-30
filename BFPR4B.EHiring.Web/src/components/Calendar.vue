<template>
  <div class="p-6">
    <div class="bg-white dark:bg-gray-800 shadow-md rounded-lg p-6">
      <h2 class="text-2xl font-bold text-gray-800 dark:text-gray-200 mb-4">
        Calendar
      </h2>

      <div class="flex items-center justify-between mb-6">
        <button @click="prevMonth"
                class="px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-opacity-50">
          <i class="fas fa-chevron-left"></i>
        </button>
        <h3 class="text-xl font-semibold text-gray-800 dark:text-gray-200">
          {{ currentMonthName }} {{ currentYear }}
        </h3>
        <button @click="nextMonth"
                class="px-4 py-2 bg-green-500 text-white rounded-lg hover:bg-green-600 focus:outline-none focus:ring-2 focus:ring-green-500 focus:ring-opacity-50">
          <i class="fas fa-chevron-right"></i>
        </button>
      </div>

      <div class="grid grid-cols-7 gap-2 text-center text-gray-600 dark:text-gray-400 font-semibold mb-4">
        <div>Sun</div>
        <div>Mon</div>
        <div>Tue</div>
        <div>Wed</div>
        <div>Thu</div>
        <div>Fri</div>
        <div>Sat</div>
      </div>

      <div class="grid grid-cols-7 gap-2">
        <div v-for="day in daysInMonth"
             :key="day ? day.date : 'empty-' + Math.random()"
             :class="{
            'p-2 rounded-lg flex flex-col items-start justify-start h-24 overflow-y-auto': true, // Added flex, h-24, overflow
            'text-gray-700 dark:text-gray-300': day && !day.isToday,
            'bg-green-500 text-white font-bold': day && day.isToday,
            'cursor-pointer hover:bg-gray-200 dark:hover:bg-gray-700': day,
            'opacity-50 cursor-not-allowed': !day,
          }"
             @click="day ? selectDate(day.date) : null">
          <span :class="{ 'self-end': true, 'text-black dark:text-white': day && day.isToday }">
            {{ day ? day.dayOfMonth : '' }}
          </span>
          <div v-if="day && day.events && day.events.length > 0" class="w-full mt-1 text-xs">
            <div v-for="event in day.events"
                 :key="event.title"
                 class="bg-green-200 dark:bg-green-700 text-green-800 dark:text-green-200 rounded px-1 py-0.5 mb-0.5 truncate">
              {{ event.title }}
            </div>
          </div>
        </div>
      </div>

    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, computed } from 'vue'; // Removed 'watch' as it's not currently used

  // Define the events prop
  const props = defineProps<{
    events?: { date: Date; title: string; }[];
  }>();

  const today = new Date();
  const currentMonth = ref(today.getMonth());
  const currentYear = ref(today.getFullYear());
  // selectedDate is no longer used for displaying a list, but can be kept for other interactions if needed
  const selectedDate = ref<Date | null>(null);

  const daysInMonth = computed(() => {
    const numDays = new Date(currentYear.value, currentMonth.value + 1, 0).getDate();
    const firstDayOfWeek = new Date(currentYear.value, currentMonth.value, 1).getDay(); // 0 for Sunday, 6 for Saturday

    const days = [];
    // Add leading empty cells for days before the 1st of the month
    for (let i = 0; i < firstDayOfWeek; i++) {
      days.push(null);
    }

    // Add actual days of the month
    for (let i = 1; i <= numDays; i++) {
      const date = new Date(currentYear.value, currentMonth.value, i);
      // Filter events for the current day
      const dayEvents = props.events?.filter(event => event.date.toDateString() === date.toDateString()) || [];
      days.push({
        date: date,
        dayOfMonth: i,
        isToday: date.toDateString() === today.toDateString(),
        events: dayEvents, // Attach events to the day object
      });
    }
    return days;
  });

  const currentMonthName = computed(() => {
    return new Date(currentYear.value, currentMonth.value).toLocaleString('en-US', { month: 'long' });
  });

  // formattedSelectedDate is no longer actively displayed in the template, but kept for potential future use
  const formattedSelectedDate = computed(() => {
    return selectedDate.value ? selectedDate.value.toLocaleDateString('en-US', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric' }) : '';
  });

  const prevMonth = () => {
    if (currentMonth.value === 0) {
      currentMonth.value = 11;
      currentYear.value--;
    } else {
      currentMonth.value--;
    }
  };

  const nextMonth = () => {
    if (currentMonth.value === 11) {
      currentMonth.value = 0;
      currentYear.value++;
    } else {
      currentMonth.value++;
    }
  };

  const selectDate = (date: Date) => {
    selectedDate.value = date;
    // This function can still be used if you want to perform other actions on date selection,
    // even if the event list is no longer displayed here.
  };
</script>

<style scoped>
  /* Adjust styles as needed for event display within cells */
</style>
