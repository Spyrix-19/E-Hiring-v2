<!-- src/components/Profile.vue -->
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
              Update your personal details and contact information
            </p>
          </div>
        </div>
        <form @submit.prevent="submitProfileUpdate" class="space-y-6">
          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Basic Information</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">First Name</label>
              <input v-model="editableProfile.firstName" type="text" required
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter first name" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Middle Name</label>
              <input v-model="editableProfile.middleName" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter middle name" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Last Name</label>
              <input v-model="editableProfile.lastName" type="text" required
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter last name" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Suffix Name</label>
              <input v-model="editableProfile.suffix" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="e.g., Jr., Sr., III" />
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Demographics</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Birthday</label>
              <input v-model="editableProfile.birthday" type="date" required @change="calculateAge"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Age</label>
              <input :value="editableProfile.age" type="number" readonly
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg bg-gray-50 dark:bg-gray-900 text-gray-600 dark:text-gray-400"
                     placeholder="Auto-calculated" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Gender</label>
              <select v-model="editableProfile.gender" required
                      class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100">
                <option value="">Select Gender</option>
                <option value="Male">Male</option>
                <option value="Female">Female</option>
                <option value="Non-binary">Non-binary</option>
                <option value="Prefer not to say">Prefer not to say</option>
              </select>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Civil Status</label>
              <select v-model="editableProfile.civilStatus" required
                      class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100">
                <option value="">Select Civil Status</option>
                <option value="Single">Single</option>
                <option value="Married">Married</option>
                <option value="Divorced">Divorced</option>
                <option value="Widowed">Widowed</option>
              </select>
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Height (cm)</label>
              <input v-model.number="editableProfile.height" type="number"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter height in cm" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Weight (kg)</label>
              <input v-model.number="editableProfile.weight" type="number"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter weight in kg" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Blood Type</label>
              <input v-model="editableProfile.bloodType" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="e.g., A+, O-" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Religion</label>
              <input v-model="editableProfile.religion" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter religion" />
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Contact Information</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Email Address</label>
              <input v-model="editableProfile.email" type="email" required
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter email address" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Contact Number</label>
              <input v-model="editableProfile.contactNumber" type="tel"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="e.g., +63917xxxxxxx" />
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Addresses</h3>
          <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Permanent Address</label>
            <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
              <input v-model="editableProfile.permanentAddress.street" type="text" placeholder="Street Address"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500" />
              <input v-model="editableProfile.permanentAddress.barangay" type="text" placeholder="Barangay"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500" />
              <input v-model="editableProfile.permanentAddress.city" type="text" placeholder="City/Municipality"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500" />
              <input v-model="editableProfile.permanentAddress.province" type="text" placeholder="Province"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500" />
            </div>
          </div>

          <div>
            <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2 flex flex-col sm:flex-row sm:items-center sm:justify-between">
              <span>Present Address</span>
              <div class="flex items-center mt-2 sm:mt-0">
                <span class="mr-2 text-sm text-gray-600 dark:text-gray-400">Same as Permanent Address</span>
                <label for="sameAddressToggle" class="relative inline-flex items-center cursor-pointer">
                  <input type="checkbox" id="sameAddressToggle" v-model="isSameAddress" class="sr-only peer">
                  <div class="w-11 h-6 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-green-300 rounded-full peer peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-5 after:w-5 after:transition-all peer-checked:bg-green-600 dark:bg-gray-600 dark:peer-checked:bg-green-700">
                  </div>
                </label>
              </div>
            </label>
            <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4">
              <input v-model="editableProfile.presentAddress.street" type="text" placeholder="Street Address"
                     :disabled="isSameAddress"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500 disabled:bg-gray-100 dark:disabled:bg-gray-800 disabled:text-gray-500 dark:disabled:text-gray-500" />
              <input v-model="editableProfile.presentAddress.barangay" type="text" placeholder="Barangay"
                     :disabled="isSameAddress"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500 disabled:bg-gray-100 dark:disabled:bg-gray-800 disabled:text-gray-500 dark:disabled:text-gray-500" />
              <input v-model="editableProfile.presentAddress.city" type="text" placeholder="City/Municipality"
                     :disabled="isSameAddress"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500 disabled:bg-gray-100 dark:disabled:bg-gray-800 disabled:text-gray-500 dark:disabled:text-gray-500" />
              <input v-model="editableProfile.presentAddress.province" type="text" placeholder="Province"
                     :disabled="isSameAddress"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500 disabled:bg-gray-100 dark:disabled:bg-gray-800 disabled:text-gray-500 dark:disabled:text-gray-500" />
            </div>
          </div>

          <h3 class="text-xl font-semibold text-gray-700 dark:text-gray-300 mt-8 mb-4">Government IDs & Other Details</h3>
          <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">TIN</label>
              <input v-model="editableProfile.tin" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter TIN" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Pag-IBIG No.</label>
              <input v-model="editableProfile.pagibig" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter Pag-IBIG number" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">GSIS No.</label>
              <input v-model="editableProfile.gsis" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter GSIS number" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">SSS No.</label>
              <input v-model="editableProfile.sss" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter SSS number" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">PhilHealth No.</label>
              <input v-model="editableProfile.philhealth" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter PhilHealth number" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">NBI Clearance No.</label>
              <input v-model="editableProfile.nbi" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter NBI Clearance number" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">NBI Issued Date</label>
              <input v-model="editableProfile.nbiIssuedDate" type="date"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500" />
            </div>
            <div>
              <label class="block text-sm font-medium text-gray-700 dark:text-gray-300 mb-2">Course</label>
              <input v-model="editableProfile.course" type="text"
                     class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
                     placeholder="Enter course/program" />
            </div>
          </div>


          <div class="flex justify-end space-x-4 mt-8">
            <button type="button"
                    class="px-6 py-3 border border-gray-300 dark:border-gray-600 text-gray-700 dark:text-gray-300 rounded-lg hover:bg-gray-50 dark:hover:bg-gray-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer">
              Cancel
            </button>
            <button type="submit"
                    class="px-6 py-3 bg-green-600 text-white rounded-lg hover:bg-green-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer">
              Save Changes
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, watch, defineProps, defineEmits, onMounted } from "vue";

  const props = defineProps({
    userProfile: {
      type: Object,
      required: true,
    },
  });

  const emit = defineEmits(['update-profile']);

  // Initialize editableProfile with default values for all fields,
  // including the new ones, and merge with existing userProfile prop.
  const editableProfile = ref({
    firstName: "",
    middleName: "",
    lastName: "",
    suffix: "",
    birthday: "",
    age: null,
    gender: "",
    height: null, // New field
    weight: null, // New field
    bloodType: "", // New field
    civilStatus: "", // New field
    religion: "", // New field
    email: "",
    contactNumber: "", // New field
    tin: "", // New field
    pagibig: "", // New field
    gsis: "", // New field
    sss: "", // New field
    philhealth: "", // New field
    nbi: "", // New field
    nbiIssuedDate: "", // New field
    course: "", // New field
    permanentAddress: { // Changed from 'address' to 'permanentAddress'
      street: "",
      barangay: "",
      city: "",
      province: "",
    },
    presentAddress: { // New address object for present address
      street: "",
      barangay: "",
      city: "",
      province: "",
    },
    ...props.userProfile // Spread existing userProfile on top to override defaults
  });

  // Reactive state for the "Same as Permanent Address" toggle
  const isSameAddress = ref(false);

  // Watch for changes in the userProfile prop and update the local copy.
  // This ensures that if the parent component updates userProfile,
  // the form reflects the latest data.
  watch(() => props.userProfile, (newVal) => {
    // Deep merge to ensure nested objects are also updated correctly
    editableProfile.value = {
      ...editableProfile.value, // Keep existing editable values
      ...newVal, // Override with new prop values
      permanentAddress: { ...newVal.permanentAddress }, // Deep copy nested objects
      presentAddress: { ...newVal.presentAddress },
    };
  }, { deep: true });

  // Watch for changes in permanentAddress and update presentAddress if isSameAddress is true.
  watch(() => editableProfile.value.permanentAddress, (newVal) => {
    if (isSameAddress.value) {
      editableProfile.value.presentAddress = { ...newVal };
    }
  }, { deep: true });

  // Watch for changes in the isSameAddress toggle.
  watch(isSameAddress, (newVal) => {
    if (newVal) {
      // If true, copy permanent address to present address
      editableProfile.value.presentAddress = { ...editableProfile.value.permanentAddress };
    } else {
      // If false, clear present address fields or allow manual entry.
      // For now, we'll clear them to indicate they are no longer linked.
      editableProfile.value.presentAddress = {
        street: "",
        barangay: "",
        city: "",
        province: "",
      };
    }
  });

  /**
   * Calculates the age based on the birthday.
   */
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
      editableProfile.value.age = null; // Set to null if birthday is not provided
    }
  };

  /**
   * Emits the updated profile data to the parent component.
   */
  const submitProfileUpdate = () => {
    emit('update-profile', editableProfile.value);
  };

  // Calculate age on component mount to initialize the age field
  onMounted(() => {
    calculateAge();
    // If the initial userProfile has presentAddress matching permanentAddress,
    // set the toggle to true. This handles cases where data is pre-filled.
    if (
      JSON.stringify(editableProfile.value.permanentAddress) ===
      JSON.stringify(editableProfile.value.presentAddress) &&
      // Ensure addresses are not all empty, meaning they were actually set to be the same
      (editableProfile.value.permanentAddress.street !== "" ||
        editableProfile.value.permanentAddress.barangay !== "" ||
        editableProfile.value.permanentAddress.city !== "" ||
        editableProfile.value.permanentAddress.province !== "")
    ) {
      isSameAddress.value = true;
    }
  });
</script>

<style scoped>
  /* No specific styles needed here, as Tailwind CSS handles the theming */
</style>
