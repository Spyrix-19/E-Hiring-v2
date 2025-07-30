<!-- src/components/Settings.vue -->
<template>
  <div class="p-6 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 min-h-screen">
    <div class="max-w-4xl mx-auto">
      <div class="mb-8">
        <h2 class="text-2xl font-bold text-gray-800 dark:text-gray-200 mb-2">Settings</h2>
        <p class="text-gray-600 dark:text-gray-400">
          Manage your account preferences and system settings
        </p>
      </div>
      <div class="space-y-6">
        <div class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-6">
          <h3 class="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-6">
            Account Settings
          </h3>
          <div class="space-y-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Email Address</label>
              <div class="flex space-x-4">
                <input v-model="editableSettings.email"
                       type="email"
                       class="flex-1 px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                       placeholder="Enter new email address" />
                <button @click="$emit('update-email', editableSettings.email)"
                        class="px-6 py-3 bg-green-600 text-white rounded-lg hover:bg-green-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer">
                  Update Email
                </button>
              </div>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Change Password</label>
              <div class="space-y-4">
                <input v-model="editableSettings.currentPassword"
                       type="password"
                       class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                       placeholder="Current password" />
                <input v-model="editableSettings.newPassword"
                       type="password"
                       class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                       placeholder="New password" />
                <input v-model="editableSettings.confirmPassword"
                       type="password"
                       class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                       placeholder="Confirm new password" />
                <button @click="$emit('update-password', editableSettings.newPassword, editableSettings.confirmPassword)"
                        class="px-6 py-3 bg-green-600 text-white rounded-lg hover:bg-green-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer">
                  Update Password
                </button>
              </div>
            </div>
          </div>
        </div>
        <div class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-6">
          <h3 class="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-6">
            Preferences
          </h3>
          <div class="space-y-6">
            <div class="flex items-center justify-between">
              <div>
                <h4 class="font-medium text-gray-800 dark:text-gray-200">Theme Mode</h4>
                <p class="text-sm text-gray-600 dark:text-gray-400">
                  Choose your preferred theme
                </p>
              </div>
              <div class="flex items-center space-x-4">
                <button @click="$emit('update-theme', 'light')"
                        :class="`px-4 py-2 rounded-lg transition-colors !rounded-button whitespace-nowrap cursor-pointer ${settings.theme === 'light' ? 'bg-green-600 text-white' : 'bg-gray-200 text-gray-700 dark:bg-gray-700 dark:text-gray-300'}`">
                  Light
                </button>
                <button @click="$emit('update-theme', 'dark')"
                        :class="`px-4 py-2 rounded-lg transition-colors !rounded-button whitespace-nowrap cursor-pointer ${settings.theme === 'dark' ? 'bg-green-600 text-white' : 'bg-gray-200 text-gray-700 dark:bg-gray-700 dark:text-gray-300'}`">
                  Dark
                </button>
              </div>
            </div>
            <div class="flex items-center justify-between">
              <div>
                <h4 class="font-medium text-gray-800 dark:text-gray-200">Language</h4>
                <p class="text-sm text-gray-600 dark:text-gray-400">
                  Select your preferred language
                </p>
              </div>
              <select v-model="editableSettings.language"
                      @change="$emit('update-language', editableSettings.language)"
                      class="px-4 py-2 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100">
                <option value="en">English</option>
                <option value="fil">Filipino</option>
              </select>
            </div>
          </div>
        </div>
        <div class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-6">
          <h3 class="text-lg font-semibold text-gray-800 dark:text-gray-200 mb-6">
            Support
          </h3>
          <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
            <button @click="$emit('open-support')"
                    class="flex items-center justify-center space-x-3 p-4 border border-gray-300 dark:border-gray-600 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors cursor-pointer">
              <i class="fas fa-headset text-green-600 dark:text-green-400"></i>
              <span class="font-medium text-gray-800 dark:text-gray-200">Contact Support</span>
            </button>
            <button @click="$emit('open-survey')"
                    class="flex items-center justify-center space-x-3 p-4 border border-gray-300 dark:border-gray-600 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors cursor-pointer">
              <i class="fas fa-poll text-green-600 dark:text-green-400"></i>
              <span class="font-medium text-gray-800 dark:text-gray-200">Take Survey</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, watch, defineProps, defineEmits } from "vue";

  const props = defineProps({
    settings: {
      type: Object,
      required: true,
      default: () => ({ // Provide a default empty object to prevent errors if settings is not passed
        email: '',
        currentPassword: '',
        newPassword: '',
        confirmPassword: '',
        theme: 'light', // Default theme
        language: 'en', // Default language
      })
    },
  });

  const emit = defineEmits([
    'update-email',
    'update-password',
    'update-theme',
    'update-language',
    'open-support',
    'open-survey',
  ]);

  // Create a local copy of the prop for editing
  const editableSettings = ref({ ...props.settings });

  // Watch for changes in the prop and update the local copy
  watch(() => props.settings, (newVal) => {
    editableSettings.value = { ...newVal };
  }, { deep: true });
</script>

<style scoped>
  /* No specific styles needed here, as Tailwind CSS handles the theming */
</style>
