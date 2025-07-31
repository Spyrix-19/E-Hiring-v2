<template>
  <div class="p-6 bg-gray-100 dark:bg-gray-900 text-gray-900 dark:text-gray-100 min-h-screen">
    <div class="w-full">
      <div class="bg-white dark:bg-gray-800 rounded-xl shadow-sm p-8">
        <div class="flex items-center space-x-6 mb-8">
          <div class="w-24 h-24 bg-gradient-to-br from-green-500 to-green-600 rounded-full flex items-center justify-center">
            <i class="fas fa-user text-white text-3xl"></i>
          </div>
          <div>
            <h2 class="text-2xl font-bold text-gray-800 dark:text-gray-200">
              Personal Information
            </h2>
            <p class="text-gray-600 dark:text-gray-400">
              View your personal details and contact information
            </p>
          </div>
        </div>
        <div class="space-y-6">
          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Basic Information</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">First Name</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.firstName || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Middle Name</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.middleName || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Last Name</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.lastName || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Suffix Name</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.suffix || 'N/A' }}
              </p>
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Demographics</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Birthday</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.birthday || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Age</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.age !== null ? editableProfile.age : 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Gender</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.gender || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Civil Status</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.civilStatus || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Height (cm)</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.height !== null ? editableProfile.height : 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Weight (kg)</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.weight !== null ? editableProfile.weight : 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Blood Type</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.bloodType || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Religion</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.religion || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Course</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.course || 'N/A' }}
              </p>
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Contact Information</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Email Address</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.email || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Contact Number</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.contactNumber || 'N/A' }}
              </p>
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Addresses</h3>
          <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Permanent Address</label>
            <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-3 gap-4">
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Street Address</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.permanentAddress.street || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Barangay</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.permanentAddress.barangay || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">City/Municipality</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.permanentAddress.city || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Province</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.permanentAddress.province || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Region</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.permanentAddress.region || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Zip Code</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.permanentAddress.zipCode || 'N/A' }}
                </p>
              </div>
            </div>
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Present Address</label>
            <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-3 gap-4">
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Street Address</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.presentAddress.street || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Barangay</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.presentAddress.barangay || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">City/Municipality</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.presentAddress.city || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Province</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.presentAddress.province || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Region</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.presentAddress.region || 'N/A' }}
                </p>
              </div>
              <div>
                <label class="block text-xs font-medium text-gray-500 dark:text-gray-400 mb-1">Zip Code</label>
                <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                  {{ editableProfile.presentAddress.zipCode || 'N/A' }}
                </p>
              </div>
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Government IDs & Other Details</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">TIN</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.tin || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Pag-IBIG No.</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.pagibig || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">GSIS No.</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.gsis || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">SSS No.</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.sss || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">PhilHealth No.</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.philhealth || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">NBI Clearance No.</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.nbi || 'N/A' }}
              </p>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">NBI Issued Date</label>
              <p class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
                {{ editableProfile.nbiIssuedDate || 'N/A' }}
              </p>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, watch, defineProps, onMounted } from "vue";

  const props = defineProps({
    userProfile: {
      type: Object,
      required: true,
    },
  });

  const editableProfile = ref({
    firstName: "",
    middleName: "",
    lastName: "",
    suffix: "",
    birthday: "",
    age: null,
    gender: "",
    height: null,
    weight: null,
    bloodType: "",
    civilStatus: "",
    religion: "",
    course: "",
    email: "",
    contactNumber: "",
    tin: "",
    pagibig: "",
    gsis: "",
    sss: "",
    philhealth: "",
    nbi: "",
    nbiIssuedDate: "",
    permanentAddress: {
      street: "",
      barangay: "",
      city: "",
      province: "",
      region: "", // New address field
      zipCode: "", // New address field
    },
    presentAddress: {
      street: "",
      barangay: "",
      city: "",
      province: "",
      region: "", // New address field
      zipCode: "", // New address field
    },
    ...props.userProfile // Spread existing userProfile on top to override defaults
  });

  watch(() => props.userProfile, (newVal) => {
    editableProfile.value = {
      ...editableProfile.value,
      ...newVal,
      permanentAddress: { ...newVal.permanentAddress },
      presentAddress: { ...newVal.presentAddress },
    };
  }, { deep: true });

  const calculateAge = () => {
    if (editableProfile.value.birthday) {
      const today = new Date();
      const birthDate = new Date(editableProfile.value.birthday);
      let age = today.getFullYear() - birthDate.getFullYear();
      const monthDiff = today.getMonth() - birthDate.getMonth();
      if (
        monthDiff < 0 ||
        (monthDiff === 0 && today.getDate() < birthDate.getDate())
      ) {
        age--;
      }
      editableProfile.value.age = age;
    } else {
      editableProfile.value.age = null;
    }
  };

  onMounted(() => {
    calculateAge();
  });
</script>

<style scoped>
  /* No specific styles needed here, as Tailwind CSS handles the theming */
</style>
