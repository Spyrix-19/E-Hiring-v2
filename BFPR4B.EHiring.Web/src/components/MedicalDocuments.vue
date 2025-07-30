<!-- src/components/MedicalDocuments.vue -->
<template>
  <div class="p-6 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 min-h-screen">
    <div class="w-full">
      <div class="mb-8">
        <h2 class="text-2xl font-bold text-gray-800 dark:text-gray-200 mb-2">
          Medical Documents
        </h2>
        <p class="text-gray-600 dark:text-gray-400">
          Upload your medical examination results and health certificates
        </p>
      </div>
      <div class="grid grid-cols-1 md:grid-cols-2 gap-8">
        <div v-for="medical in medicalDocuments"
             :key="medical.id"
             class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-8 border border-gray-200 dark:border-gray-700">
          <div class="text-center mb-6">
            <div :class="`w-20 h-20 rounded-full flex items-center justify-center mx-auto mb-4
                          ${medical.uploaded ? 'bg-green-100 dark:bg-green-800/20' : 'bg-red-100 dark:bg-red-800/20'}`">
              <i :class="`fas ${medical.icon} text-2xl
                          ${medical.uploaded ? 'text-green-600 dark:text-green-400' : 'text-red-600 dark:text-red-400'}`"></i>
            </div>
            <h3 class="text-xl font-semibold text-gray-800 dark:text-gray-200 mb-2">
              {{ medical.name }}
            </h3>
            <p class="text-gray-600 dark:text-gray-400 mb-4">{{ medical.description }}</p>
            <div :class="`inline-block px-4 py-2 rounded-full text-sm font-medium
                          ${medical.uploaded ? 'bg-green-100 text-green-800 dark:bg-green-800/30 dark:text-green-300' : 'bg-red-100 text-red-800 dark:bg-red-800/30 dark:text-red-300'}`">
              {{ medical.uploaded ? 'Completed' : 'Pending' }}
            </div>
          </div>
          <div class="space-y-3">
            <button @click="uploadMedicalDocument(medical.id)"
                    :disabled="uploadingMedicalDocId === medical.id"
                    class="w-full bg-green-600 text-white py-3 px-4 rounded-lg hover:bg-green-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer relative">
              <span v-if="uploadingMedicalDocId === medical.id"
                    class="flex items-center justify-center">
                <i class="fas fa-spinner fa-spin mr-2"></i>
                Uploading...
              </span>
              <span v-else>
                {{
                  medical.uploaded ? 'Update Results' : 'Upload Results'
                }}
              </span>
            </button>
            <button v-if="medical.uploaded"
                    @click="viewMedicalResults(medical.id)"
                    class="w-full border border-gray-300 dark:border-gray-600 text-gray-700 dark:text-gray-300 py-3 px-4 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer">
              View Results
            </button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, defineProps, defineEmits } from "vue";

  const props = defineProps({
    medicalDocuments: {
      type: Array,
      required: true,
      default: () => [] // Provide a default empty array for safety
    },
  });

  const emit = defineEmits(['medical-document-uploaded']);

  const uploadingMedicalDocId = ref<number | null>(null);

  const uploadMedicalDocument = async (docId: number) => {
    uploadingMedicalDocId.value = docId;
    try {
      // Simulate upload delay
      await new Promise((resolve) => setTimeout(resolve, 2000));
      emit('medical-document-uploaded', docId); // Emit event to parent to update global state
    } finally {
      uploadingMedicalDocId.value = null;
    }
  };

  const viewMedicalResults = (docId: number) => {
    // IMPORTANT: Replaced alert() with a console log as alert() is not supported in the Canvas environment.
    // For a real application, you would implement a custom modal or toast notification here.
    console.log(`Viewing medical results for document ID: ${docId}`);
  };
</script>

<style scoped>
  /* Add any specific styles here if needed. Tailwind CSS is primarily used for styling. */
</style>
