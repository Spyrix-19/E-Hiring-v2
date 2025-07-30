<!-- src/components/ESumbong.vue -->
<template>
  <div class="p-6 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 min-h-screen">
    <div class="w-full">
      <div class="mb-8">
        <h2 class="text-2xl font-bold text-gray-800 dark:text-gray-200 mb-2">E-Sumbong</h2>
        <p class="text-gray-600 dark:text-gray-400">
          Submit complaints or feedback regarding the hiring process
        </p>
      </div>
      <div class="grid grid-cols-1 lg:grid-cols-3 gap-8">
        <div class="lg:col-span-1 bg-white dark:bg-gray-800 rounded-xl shadow-sm p-6">
          <h3 class="text-xl font-semibold text-gray-800 dark:text-gray-200 mb-6">
            Submit New Complaint
          </h3>
          <form @submit.prevent="submitComplaint" class="space-y-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Category</label>
              <select v-model="complaintForm.category"
                      required
                      class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100">
                <option value="">Select category</option>
                <option value="process">Application Process</option>
                <option value="technical">Technical Issues</option>
                <option value="staff">Staff Conduct</option>
                <option value="other">Other</option>
              </select>
            </div>

            <div v-if="complaintForm.category === 'other'">
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Specify Category</label>
              <input v-model="complaintForm.otherCategory"
                     type="text"
                     required
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="e.g., Website Bug, Interview Feedback" />
            </div>

            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Subject</label>
              <input v-model="complaintForm.subject"
                     type="text"
                     required
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Brief description of your concern" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Description</label>
              <textarea v-model="complaintForm.description"
                        required
                        rows="5"
                        class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent resize-none bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                        placeholder="Provide detailed information about your complaint"></textarea>
            </div>
            <button type="submit"
                    class="w-full bg-red-600 text-white py-3 px-4 rounded-lg hover:bg-red-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer">
              Submit Complaint
            </button>
          </form>
        </div>
        <div class="lg:col-span-2 bg-white dark:bg-gray-800 rounded-xl shadow-sm p-6">
          <h3 class="text-xl font-semibold text-gray-800 dark:text-gray-200 mb-6">
            Complaint History
          </h3>
          <div class="space-y-4">
            <div v-for="complaint in complaintHistory"
                 :key="complaint.id"
                 class="border border-gray-200 dark:border-gray-700 rounded-lg p-4">
              <div class="flex items-center justify-between mb-2">
                <h4 class="font-medium text-gray-800 dark:text-gray-200">
                  {{ complaint.subject }}
                </h4>
                <span :class="`px-2 py-1 rounded-full text-xs font-medium ${getComplaintStatusColor(complaint.status)}`">
                  {{ complaint.status }}
                </span>
              </div>
              <p class="text-gray-600 dark:text-gray-400 text-sm mb-2">
                {{ complaint.description }}
              </p>
              <div class="flex items-center justify-between text-xs text-gray-500 dark:text-gray-400">
                <span>{{ complaint.category }}<span v-if="complaint.otherCategory"> ({{ complaint.otherCategory }})</span></span>
                <span>{{ complaint.date }}</span>
              </div>
            </div>
            <p v-if="complaintHistory.length === 0" class="text-gray-500 dark:text-gray-400 text-center">No complaints submitted yet.</p>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, defineProps, defineEmits } from "vue";

  const props = defineProps({
    complaintHistory: {
      type: Array,
      required: true,
      default: () => [] // Provide a default empty array for safety
    },
  });

  const emit = defineEmits(['submit-complaint']);

  const complaintForm = ref({
    category: "",
    subject: "",
    description: "",
    otherCategory: "",
  });

  const submitComplaint = () => {
    const complaintData = {
      ...complaintForm.value,
      id: Date.now(),
      date: new Date().toLocaleDateString('en-US'), // Use 'en-US' for consistent date format or adjust as needed
      status: 'Submitted'
    };

    if (complaintForm.value.category !== 'other') {
      complaintData.otherCategory = '';
    }

    emit('submit-complaint', complaintData);

    complaintForm.value = { category: "", subject: "", description: "", otherCategory: "" };

    console.log('Complaint submitted successfully! (This would be a SweetAlert/Toaster)');
  };

  const getComplaintStatusColor = (status: string) => {
    // These colors are defined to work across both light and dark modes
    // You might want to fine-tune the dark mode specific text colors if needed,
    // but the background colors already provide good contrast.
    const colors: { [key: string]: string } = {
      Submitted: "bg-green-100 text-green-800 dark:bg-green-800/30 dark:text-green-300",
      "In Progress": "bg-yellow-100 text-yellow-800 dark:bg-yellow-800/30 dark:text-yellow-300",
      Resolved: "bg-green-100 text-green-800 dark:bg-green-800/30 dark:text-green-300",
      Closed: "bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300",
    };
    return colors[status] || "bg-gray-100 text-gray-800 dark:bg-gray-700 dark:text-gray-300";
  };
</script>

<style scoped>
  /* No specific styles needed here, as Tailwind CSS handles the theming */
</style>
