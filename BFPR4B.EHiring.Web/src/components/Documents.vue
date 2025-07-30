<!-- src/components/Documents.vue -->
<template>
  <div class="p-6 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 min-h-screen">
    <div class="w-full">
      <div class="mb-8">
        <h2 class="text-2xl font-bold text-gray-800 dark:text-gray-200 mb-2">
          Document Management
        </h2>
        <p class="text-gray-600 dark:text-gray-400">
          Upload and manage your required documents for the application process
        </p>
      </div>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <div v-for="doc in requiredDocuments"
             :key="doc.id"
             class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-6 border border-gray-200 dark:border-gray-700 hover:shadow-md transition-shadow">
          <div class="flex items-center justify-between mb-4">
            <div :class="`w-12 h-12 ${getIconStyle(doc.status)} rounded-lg flex items-center justify-center`">
              <i :class="`fas ${doc.icon} ${getIconTextStyle(doc.status)} text-xl`"></i>
            </div>
            <div :class="`px-3 py-1 rounded-full text-xs font-medium ${getStatusStyle(doc.status)}`">
              {{ getStatusText(doc.status) }}
            </div>
          </div>
          <h3 class="font-semibold text-gray-800 dark:text-gray-200 mb-2">{{ doc.name }}</h3>
          <p class="text-sm text-gray-600 dark:text-gray-400 mb-4">{{ doc.description }}</p>
          <div class="space-y-2">
            <button @click="uploadDocument(doc.id)"
                    :disabled="uploadingDocId === doc.id"
                    class="w-full bg-green-600 text-white py-2 px-4 rounded-lg hover:bg-green-700 transition-colors text-sm !rounded-button whitespace-nowrap cursor-pointer relative">
              <span v-if="uploadingDocId === doc.id"
                    class="flex items-center justify-center">
                <i class="fas fa-spinner fa-spin mr-2"></i>
                Uploading...
              </span>
              <span v-else>
                {{
                  doc.uploaded ? 'Replace Document' : 'Upload Document'
                }}
              </span>
            </button>
            <button v-if="doc.uploaded"
                    @click="previewDocument(doc.id)"
                    class="w-full border border-gray-300 dark:border-gray-600 text-gray-700 dark:text-gray-300 py-2 px-4 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors text-sm !rounded-button whitespace-nowrap cursor-pointer">
              Preview Document
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
    requiredDocuments: {
      type: Array,
      required: true,
      default: () => [] // Provide a default empty array for safety
    },
  });

  const emit = defineEmits(['document-uploaded']);

  const uploadingDocId = ref<number | null>(null);

  const getIconStyle = (status: string) => {
    const styles: { [key: string]: string } = {
      pending: "bg-yellow-100 dark:bg-yellow-800/20",
      uploaded: "bg-green-100 dark:bg-green-800/20",
      processing: "bg-purple-100 dark:bg-purple-800/20",
      approved: "bg-green-100 dark:bg-green-800/20",
      rejected: "bg-red-100 dark:bg-red-800/20",
    };
    return styles[status] || "bg-gray-100 dark:bg-gray-700";
  };

  const getIconTextStyle = (status: string) => {
    const styles: { [key: string]: string } = {
      pending: "text-yellow-600 dark:text-yellow-400",
      uploaded: "text-green-600 dark:text-green-400",
      processing: "text-purple-600 dark:text-purple-400",
      approved: "text-green-600 dark:text-green-400",
      rejected: "text-red-600 dark:text-red-400",
    };
    return styles[status] || "text-gray-400 dark:text-gray-500";
  };

  const getStatusStyle = (status: string) => {
    const styles: { [key: string]: string } = {
      pending: "bg-yellow-100 text-yellow-800 dark:bg-yellow-800/30 dark:text-yellow-300",
      uploaded: "bg-green-100 text-green-800 dark:bg-green-800/30 dark:text-green-300",
      processing: "bg-purple-100 text-purple-800 dark:bg-purple-800/30 dark:text-purple-300",
      approved: "bg-green-100 text-green-800 dark:bg-green-800/30 dark:text-green-300",
      rejected: "bg-red-100 text-red-800 dark:bg-red-800/30 dark:text-red-300",
    };
    return styles[status] || "bg-gray-100 text-gray-600 dark:bg-gray-700 dark:text-gray-300";
  };

  const getStatusText = (status: string) => {
    const texts: { [key: string]: string } = {
      pending: "Pending Upload",
      uploaded: "Uploaded",
      processing: "In Review",
      approved: "Approved",
      rejected: "Rejected",
    };
    return texts[status] || "Not Started";
  };

  const uploadDocument = async (docId: number) => {
    uploadingDocId.value = docId;
    try {
      // Simulate upload delay
      await new Promise((resolve) => setTimeout(resolve, 2000));
      emit('document-uploaded', docId); // Emit event to parent to update global state
    } finally {
      uploadingDocId.value = null;
    }
  };

  const previewDocument = (docId: number) => {
    console.log(`Previewing document ID: ${docId}`);
  };
</script>

<style scoped>
  /* No specific styles needed here, as Tailwind CSS handles the theming */
</style>
