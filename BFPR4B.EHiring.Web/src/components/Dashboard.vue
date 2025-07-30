<!-- src/components/Dashboard.vue -->
<template>
  <div class="p-6 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 min-h-screen">
    <div class="bg-gradient-to-r from-green-600 to-green-800 rounded-2xl p-8 text-white mb-8 relative overflow-hidden dark:from-green-800 dark:to-green-950">
      <div class="absolute inset-0 bg-cover bg-center opacity-20 dark:opacity-10"
           :style="{ backgroundImage: `url('https://readdy.ai/api/search-image?query=Professional%20Filipino%20firefighter%20in%20uniform%20standing%20proudly%20with%20modern%20fire%20station%20background%20representing%20career%20opportunity%20and%20public%20service%20dedication%20clean%20inspiring%20composition&width=800&height=300&seq=hero001&orientation=landscape')` }"></div>
      <div class="relative z-10">
        <h2 class="text-3xl font-bold mb-2">
          Welcome back, {{ userProfile.firstName }}!
        </h2>
        <p class="text-green-100 dark:text-green-200 mb-4">
          Track your application progress and stay updated with the latest
          announcements.
        </p>
        <div class="flex items-center space-x-6">
          <div class="text-center">
            <div class="text-2xl font-bold">
              {{ applicationProgress }}%
            </div>
            <div class="text-sm text-green-100 dark:text-green-200">Application Progress</div>
          </div>
          <div class="text-center">
            <div class="text-2xl font-bold">
              {{ documentsUploaded }}/{{ totalDocuments }}
            </div>
            <div class="text-sm text-green-100 dark:text-green-200">Documents Uploaded</div>
          </div>
        </div>
      </div>
    </div>
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
      <div v-for="link in quickLinks"
           :key="link.id"
           @click="$emit('change-section', link.section)"
           class="bg-white dark:bg-gray-800 rounded-xl p-6 shadow-sm hover:shadow-md transition-shadow cursor-pointer border border-gray-100 dark:border-gray-700">
        <div class="flex items-center justify-between mb-4">
          <div :class="`w-12 h-12 ${link.bgColor} rounded-lg flex items-center justify-center`">
            <i :class="`${link.icon} text-white text-xl`"></i>
          </div>
          <i class="fas fa-arrow-right text-gray-400 dark:text-gray-500"></i>
        </div>
        <h3 class="font-semibold text-gray-800 dark:text-gray-200 mb-2">{{ link.title }}</h3>
        <p class="text-sm text-gray-600 dark:text-gray-400">{{ link.description }}</p>
      </div>
    </div>
    <div class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-6">
      <div class="flex items-center justify-between mb-6">
        <h3 class="text-xl font-bold text-gray-800 dark:text-gray-200">
          Latest Announcements
        </h3>
        <button class="text-green-600 hover:text-green-800 text-sm cursor-pointer dark:text-green-400 dark:hover:text-green-300">
          View All
        </button>
      </div>
      <div class="space-y-4">
        <div v-for="announcement in announcements"
             :key="announcement.id"
             class="border border-gray-200 dark:border-gray-700 rounded-lg p-4 hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors">
          <div class="flex items-start space-x-4">
            <div :class="`w-2 h-2 rounded-full mt-2 ${announcement.priority === 'high' ? 'bg-red-500' : announcement.priority === 'medium' ? 'bg-yellow-500' : 'bg-green-500'}`"></div>
            <div class="flex-1">
              <h4 class="font-medium text-gray-800 dark:text-gray-200 mb-1">
                {{ announcement.title }}
              </h4>
              <p class="text-gray-600 dark:text-gray-400 text-sm mb-2">
                {{ announcement.content }}
              </p>
              <div class="flex items-center text-xs text-gray-500 dark:text-gray-400">
                <i class="fas fa-calendar-alt mr-1"></i>
                {{ announcement.date }}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { defineProps, defineEmits } from 'vue';

  defineProps({
    userProfile: {
      type: Object,
      required: true,
      default: () => ({ firstName: 'User' }) // Default for safety
    },
    applicationProgress: {
      type: Number,
      required: true,
      default: 0
    },
    documentsUploaded: {
      type: Number,
      required: true,
      default: 0
    },
    totalDocuments: {
      type: Number,
      required: true,
      default: 0
    },
    quickLinks: {
      type: Array,
      required: true,
      default: () => []
    },
    announcements: {
      type: Array,
      required: true,
      default: () => []
    },
  });

  defineEmits(['change-section']);
</script>
