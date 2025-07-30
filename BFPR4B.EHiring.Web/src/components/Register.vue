<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100 dark:bg-gray-900 relative overflow-hidden">
    <div class="relative z-10 bg-white rounded-2xl shadow-2xl p-8 w-full mx-4 sm:p-10 md:p-12 max-w-4xl dark:bg-gray-800">
      <div class="mb-8 flex items-center">
        <i class="fas fa-fire text-green-600 text-4xl mr-4"></i>
        <div>
          <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-100">Create Account</h1>
          <p class="text-gray-600 dark:text-gray-400">Join BFP MIMAROPA E-Hiring Now</p>
        </div>
      </div>
      <form @submit.prevent="handleRegister" class="space-y-6">

        <div class="grid grid-cols-1 lg:grid-cols-2 gap-6">
          <div class="bg-gray-50 dark:bg-gray-700 p-6 rounded-lg shadow-inner">
            <h2 class="text-xl font-semibold text-gray-800 mb-4 dark:text-gray-100">Basic Information</h2>
            <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Last Name</label>
                <input v-model="registerForm.lastName"
                       type="text"
                       required
                       class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                       placeholder="Enter your last name" />
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">First Name</label>
                <input v-model="registerForm.firstName"
                       type="text"
                       required
                       class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                       placeholder="Enter your first name" />
              </div>
            </div>

            <div class="mt-4">
              <div class="flex items-center justify-between mb-2">
                <label class="block text-sm font-medium text-gray-700 dark:text-gray-300">Middle Name</label>
                <div class="flex items-center">
                  <span class="text-xs text-gray-600 dark:text-gray-400 mr-2">No Middle Name</span>
                  <label class="relative inline-flex items-center cursor-pointer">
                    <input type="checkbox" v-model="noMiddleName" class="sr-only peer">
                    <div class="w-9 h-5 bg-gray-200 peer-focus:outline-none peer-focus:ring-4 peer-focus:ring-green-300 dark:peer-focus:ring-green-800 rounded-full peer dark:bg-gray-700 peer-checked:after:translate-x-full peer-checked:after:border-white after:content-[''] after:absolute after:top-[2px] after:left-[2px] after:bg-white after:border-gray-300 after:border after:rounded-full after:h-4 after:w-4 after:transition-all dark:border-gray-600 peer-checked:bg-green-600"></div>
                  </label>
                </div>
              </div>
              <input v-model="registerForm.middleName"
                     type="text"
                     :disabled="noMiddleName"
                     :class="{'opacity-60 cursor-not-allowed': noMiddleName, 'w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400': true}"
                     placeholder="Enter your middle name" />
            </div>

            <div class="grid grid-cols-1 md:grid-cols-2 gap-4 mt-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Suffix (Optional)</label>
                <input v-model="registerForm.suffix"
                       type="text"
                       class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                       placeholder="e.g., Jr., Sr." />
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Birthdate</label>
                <input v-model="registerForm.birthdate"
                       type="date"
                       required
                       class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400" />
              </div>
            </div>
          </div>

          <div class="bg-gray-50 dark:bg-gray-700 p-6 rounded-lg shadow-inner">
            <h2 class="text-xl font-semibold text-gray-800 mb-4 dark:text-gray-100">Contact Information</h2>
            <!-- Removed md:grid-cols-2 here to make them stack vertically -->
            <div class="grid grid-cols-1 gap-4">
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Email Address</label>
                <input v-model="registerForm.email"
                       type="email"
                       required
                       class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                       placeholder="Enter your email address" />
              </div>
              <div>
                <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Contact Number</label>
                <input v-model="registerForm.contactNo"
                       type="tel"
                       required
                       class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                       placeholder="e.g., 09171234567" />
              </div>
            </div>
          </div>
        </div>

        <div class="bg-gray-50 dark:bg-gray-700 p-6 rounded-lg shadow-inner">
          <h2 class="text-xl font-semibold text-gray-800 mb-4 dark:text-gray-100">Set Your Credentials</h2>
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Password</label>
            <input v-model="registerForm.password"
                   type="password"
                   required
                   @input="checkPasswordStrength"
                   class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                   placeholder="Create a strong password" />
            <div class="mt-2 text-xs grid grid-cols-2 gap-1 text-gray-600 dark:text-gray-400">
              <p :class="{'text-green-500 font-semibold': passwordStrength.hasUpperCase, 'text-red-500': !passwordStrength.hasUpperCase}">
                <i :class="passwordStrength.hasUpperCase ? 'fas fa-check-circle' : 'fas fa-times-circle'" class="mr-1"></i> Uppercase (A-Z)
              </p>
              <p :class="{'text-green-500 font-semibold': passwordStrength.hasLowerCase, 'text-red-500': !passwordStrength.hasLowerCase}">
                <i :class="passwordStrength.hasLowerCase ? 'fas fa-check-circle' : 'fas fa-times-circle'" class="mr-1"></i> Lowercase (a-z)
              </p>
              <p :class="{'text-green-500 font-semibold': passwordStrength.hasNumber, 'text-red-500': !passwordStrength.hasNumber}">
                <i :class="passwordStrength.hasNumber ? 'fas fa-check-circle' : 'fas fa-times-circle'" class="mr-1"></i> Number (0-9)
              </p>
              <p :class="{'text-green-500 font-semibold': passwordStrength.hasSpecialChar, 'text-red-500': !passwordStrength.hasSpecialChar}">
                <i :class="passwordStrength.hasSpecialChar ? 'fas fa-check-circle' : 'fas fa-times-circle'" class="mr-1"></i> Special Character (!@#$)
              </p>
              <p :class="{'text-green-500 font-semibold': passwordStrength.minLength, 'text-red-500': !passwordStrength.minLength}">
                <i :class="passwordStrength.minLength ? 'fas fa-check-circle' : 'fas fa-times-circle'" class="mr-1"></i> Minimum 8 Characters
              </p>
            </div>
            <p v-if="passwordStrength.isStrong && registerForm.password.length > 0" class="text-green-600 text-sm font-semibold mt-2">
              Password strength: Strong
            </p>
            <p v-else-if="registerForm.password.length > 0 && !passwordStrength.isStrong" class="text-red-600 text-sm font-semibold mt-2">
              Password strength: Weak - Please meet all requirements.
            </p>
          </div>
          <div class="mt-4">
            <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Confirm Password</label>
            <input v-model="registerForm.confirmPassword"
                   type="password"
                   required
                   class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                   placeholder="Confirm your password" />
            <p v-if="registerForm.password && registerForm.confirmPassword && registerForm.password !== registerForm.confirmPassword"
               class="text-red-500 text-sm mt-2">
              Passwords do not match.
            </p>
          </div>
        </div>

        <div v-if="showRegisterOtp" class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Verification Code</label>
            <input v-model="registerForm.otp"
                   type="text"
                   maxlength="6"
                   class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm text-center tracking-widest dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                   placeholder="Enter 6-digit code" />
          </div>
          <p class="text-sm text-gray-600 text-center dark:text-gray-400">
            Verification code sent to {{ registerForm.email }}
          </p>
        </div>

        <div class="space-y-4 pt-4">
          <div class="flex items-start">
            <input type="checkbox"
                   v-model="registerForm.agreeTerms"
                   required
                   class="mt-1 rounded border-gray-300 text-green-600 focus:ring-green-500 dark:bg-gray-700 dark:border-gray-600 dark:checked:bg-green-600 dark:checked:border-transparent" />
            <label class="ml-2 text-sm text-gray-600 dark:text-gray-400">
              I agree to the
              <span class="text-green-600 cursor-pointer dark:text-green-400">Terms and Conditions</span>
              and
              <span class="text-green-600 cursor-pointer dark:text-green-400">Privacy Policy</span>
            </label>
          </div>
          <button type="submit"
                  :disabled="!passwordStrength.isStrong || (registerForm.password !== registerForm.confirmPassword && registerForm.confirmPassword.length > 0) || !registerForm.agreeTerms"
                  class="w-full bg-green-600 text-white py-3 rounded-lg hover:bg-green-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer disabled:opacity-50 disabled:cursor-not-allowed">
            {{ showRegisterOtp ? 'Verify Account' : 'Create Account' }}
          </button>
          <div class="mt-6 text-center">
            <p class="text-gray-600 dark:text-gray-400">Already have an account?</p>
            <button @click="$emit('login-click')"
                    class="text-green-600 hover:text-green-800 font-medium cursor-pointer dark:text-green-400 dark:hover:text-green-300">
              Login Here
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, watch, reactive } from "vue";

  const emit = defineEmits(['register-success', 'login-click']);

  const registerForm = ref({
    lastName: "",
    firstName: "",
    middleName: "",
    suffix: "",
    birthdate: "",
    contactNo: "",
    email: "",
    password: "",
    confirmPassword: "",
    otp: "",
    agreeTerms: false,
  });

  const showRegisterOtp = ref(false);
  const noMiddleName = ref(false);

  // Reactive object for password strength criteria
  const passwordStrength = reactive({
    minLength: false,
    hasUpperCase: false,
    hasLowerCase: false,
    hasNumber: false,
    hasSpecialChar: false,
    isStrong: false,
  });

  // Watch for changes in noMiddleName to clear middleName field
  watch(noMiddleName, (newValue) => {
    if (newValue) {
      registerForm.value.middleName = "";
    }
  });

  // Watch for password changes to update strength
  watch(() => registerForm.value.password, (newPassword) => {
    checkPasswordStrength();
  });

  const checkPasswordStrength = () => {
    const password = registerForm.value.password;

    passwordStrength.minLength = password.length >= 8;
    passwordStrength.hasUpperCase = /[A-Z]/.test(password);
    passwordStrength.hasLowerCase = /[a-z]/.test(password);
    passwordStrength.hasNumber = /\d/.test(password);
    passwordStrength.hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password); // Common special characters

    // Determine overall strength
    passwordStrength.isStrong =
      passwordStrength.minLength &&
      passwordStrength.hasUpperCase &&
      passwordStrength.hasLowerCase &&
      passwordStrength.hasNumber &&
      passwordStrength.hasSpecialChar;
  };

  const handleRegister = () => {
    if (registerForm.value.password !== registerForm.value.confirmPassword) {
      alert("Passwords do not match!"); // Replace with a custom modal/toast
      return;
    }
    if (!passwordStrength.isStrong) {
      alert("Please create a strong password meeting all requirements."); // Or display a more friendly message
      return;
    }
    if (!registerForm.value.agreeTerms) {
      alert("You must agree to the Terms and Conditions and Privacy Policy.");
      return;
    }

    if (!showRegisterOtp.value) {
      showRegisterOtp.value = true;
      // In a real app, you'd send registration data to backend and request OTP
      console.log("Sending registration data and requesting OTP:", registerForm.value);
    } else {
      // In a real app, you'd verify OTP and finalize registration
      console.log("Verifying OTP:", registerForm.value.otp);
      emit('register-success');
      showRegisterOtp.value = false; // Reset for next time
      // Reset form after successful registration
      registerForm.value = {
        lastName: "",
        firstName: "",
        middleName: "",
        suffix: "",
        birthdate: "",
        contactNo: "",
        email: "",
        password: "",
        confirmPassword: "",
        otp: "",
        agreeTerms: false,
      };
      noMiddleName.value = false;
      checkPasswordStrength(); // Reset password strength indicators
    }
  };
</script>
