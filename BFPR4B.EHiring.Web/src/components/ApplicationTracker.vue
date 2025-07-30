<!-- src/components/ApplicationTracker.vue -->
<template>
  <div class="p-6 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 min-h-screen">
    <div class="max-w-4xl mx-auto">
      <div class="mb-8">
        <h2 class="text-2xl font-bold text-gray-800 dark:text-gray-200 mb-2">
          Application Tracker
        </h2>
        <p class="text-gray-600 dark:text-gray-400">
          Monitor your application progress through each stage of the
          hiring process
        </p>
      </div>
      <div class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-8">
        <div class="mb-8">
          <div class="flex items-center justify-between mb-4">
            <h3 class="text-lg font-semibold text-gray-800 dark:text-gray-200">
              Overall Progress
            </h3>
            <span class="text-2xl font-bold text-green-600 dark:text-green-400">
              {{
                Math.round((currentStage / applicationStages.length) *
                100)
              }}%
            </span>
          </div>
          <div class="w-full bg-gray-200 dark:bg-gray-700 rounded-full h-3">
            <div class="bg-gradient-to-r from-green-500 to-green-600 h-3 rounded-full transition-all duration-500
                        dark:from-green-700 dark:to-green-800"
                 :style="{ width: `${(currentStage / applicationStages.length) * 100}%` }"></div>
          </div>
        </div>
        <div class="space-y-6">
          <div v-for="(stage, index) in applicationStages"
               :key="stage.id"
               class="flex items-start space-x-4">
            <div class="flex flex-col items-center">
              <div :class="`w-12 h-12 rounded-full flex items-center justify-center ${getStageStatus(index).bgColor}`">
                <i :class="`fas ${getStageStatus(index).icon} ${getStageStatus(index).textColor} text-lg`"></i>
              </div>
              <div v-if="index < applicationStages.length - 1"
                   :class="`w-0.5 h-16 mt-2 ${index < currentStage ? 'bg-green-500 dark:bg-green-600' : 'bg-gray-300 dark:bg-gray-600'}`"></div>
            </div>
            <div class="flex-1 pb-8">
              <div class="flex items-center justify-between mb-2">
                <h4 class="font-semibold text-gray-800 dark:text-gray-200">
                  {{ stage.name }}
                </h4>
                <span :class="`px-3 py-1 rounded-full text-xs font-medium ${getStageStatus(index).statusColor}`">
                  {{ getStageStatus(index).status }}
                </span>
              </div>
              <p class="text-gray-600 dark:text-gray-400 text-sm mb-3">
                {{ stage.description }}
              </p>
              <div class="space-y-2">
                <div v-if="stage.date"
                     class="flex items-center text-xs text-gray-500 dark:text-gray-400">
                  <i class="fas fa-calendar-alt mr-2"></i>
                  {{ stage.date }}
                </div>
                <div v-if="stage.note" class="flex items-start text-xs">
                  <i class="fas fa-sticky-note mt-1 mr-2 text-blue-500 dark:text-blue-400"></i>
                  <span class="text-blue-600 dark:text-blue-400">Note: {{ stage.note }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { defineProps } from "vue";

  const props = defineProps({
    currentStage: {
      type: Number,
      required: true,
      default: 0 // Added default for safety
    },
    applicationStages: {
      type: Array,
      required: true,
      default: () => [] // Added default for safety
    },
  });

  const getStageStatus = (index: number) => {
    // Ensure that applicationStages[index] is treated as an object with a 'status' property
    const stage: { status?: string } = props.applicationStages[index] as { status?: string };

    if (stage && stage.status === "completed") {
      return {
        bgColor: "bg-green-100 dark:bg-green-800/20",
        textColor: "text-green-600 dark:text-green-400",
        icon: "fa-check",
        status: "Completed",
        statusColor: "bg-green-100 text-green-800 dark:bg-green-800/30 dark:text-green-300",
      };
    } else if (stage && stage.status === "in_progress") {
      return {
        bgColor: "bg-blue-100 dark:bg-blue-800/20",
        textColor: "text-blue-600 dark:text-blue-400",
        icon: "fa-clock",
        status: "In Progress",
        statusColor: "bg-blue-100 text-blue-800 dark:bg-blue-800/30 dark:text-blue-300",
      };
    } else if (stage && stage.status === "disqualified") {
      return {
        bgColor: "bg-red-100 dark:bg-red-800/20",
        textColor: "text-red-600 dark:text-red-400",
        icon: "fa-times",
        status: "Disqualified",
        statusColor: "bg-red-100 text-red-800 dark:bg-red-800/30 dark:text-red-300",
      };
    } else {
      // Default for 'pending' or any other status
      return {
        bgColor: "bg-gray-100 dark:bg-gray-700",
        textColor: "text-gray-400 dark:text-gray-400", // Adjusted for dark mode
        icon: "fa-circle",
        status: "Pending",
        statusColor: "bg-gray-100 text-gray-600 dark:bg-gray-700 dark:text-gray-300",
      };
    }
  };
</script>
