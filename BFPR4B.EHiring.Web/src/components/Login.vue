<template>
  <div class="min-h-screen flex items-center justify-center bg-gray-100 dark:bg-gray-900">
    <div class="relative z-10 bg-white rounded-2xl shadow-2xl p-8 w-full max-w-md mx-4 dark:bg-gray-800">
      <div class="text-center mb-8">
        <div class="w-20 h-20 bg-green-600 rounded-full flex items-center justify-center mx-auto mb-4">
          <i class="fas fa-shield-alt text-white text-2xl"></i>
        </div>
        <h1 class="text-2xl font-bold text-gray-800 mb-2 dark:text-gray-100">
          BFP MIMAROPA E-Hiring
        </h1>
        <p class="text-gray-600 dark:text-gray-400">Bureau of Fire Protection MIMAROPA Region</p>
      </div>
      <form @submit.prevent="handleLogin" class="space-y-6">
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Email Address</label>
          <div class="relative">
            <input v-model="loginForm.email"
                   type="email"
                   required
                   class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                   placeholder="Enter your email address" />
            <i class="fas fa-envelope absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 text-sm dark:text-gray-500"></i>
          </div>
        </div>
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">Password</label>
          <div class="relative">
            <input v-model="loginForm.password"
                   :type="showPassword ? 'text' : 'password'"
                   required
                   class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm pr-10 dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                   placeholder="Enter your password" />
            <button type="button"
                    @click="showPassword = !showPassword"
                    class="absolute right-3 top-1/2 transform -translate-y-1/2 text-gray-400 text-sm cursor-pointer dark:text-gray-500">
              <i :class="showPassword ? 'fas fa-eye-slash' : 'fas fa-eye'"></i>
            </button>
          </div>
        </div>
        <div v-if="showOtpField" class="space-y-4">
          <div>
            <label class="block text-sm font-medium text-gray-700 mb-2 dark:text-gray-300">OTP Code</label>
            <input v-model="loginForm.otp"
                   type="text"
                   maxlength="6"
                   class="w-full px-4 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent text-sm text-center tracking-widest dark:bg-gray-700 dark:border-gray-600 dark:text-gray-100 dark:placeholder-gray-400"
                   placeholder="Enter 6-digit OTP" />
          </div>
          <p class="text-sm text-gray-600 text-center dark:text-gray-400">
            OTP sent to your email. Check your inbox.
          </p>
          <div class="flex justify-center items-center text-sm text-gray-600 dark:text-gray-400">
            <span v-if="!canResendOtp">Resend OTP in {{ formatTime }}</span>
            <button type="button"
                    @click="resendOtp"
                    :disabled="!canResendOtp"
                    :class="{'opacity-50 cursor-not-allowed': !canResendOtp, 'text-green-600 hover:text-green-800 dark:text-green-400 dark:hover:text-green-300': canResendOtp}"
                    class="ml-2 font-medium">
              Resend OTP
            </button>
          </div>
        </div>
        <div class="flex items-center justify-between">
          <label class="flex items-center">
            <input type="checkbox"
                   v-model="loginForm.rememberMe"
                   class="rounded border-gray-300 text-green-600 focus:ring-green-500 dark:bg-gray-700 dark:border-gray-600 dark:checked:bg-green-600 dark:checked:border-transparent" />
            <span class="ml-2 text-sm text-gray-600 dark:text-gray-400">Remember me</span>
          </label>
          <button type="button"
                  class="text-sm text-green-600 hover:text-green-800 cursor-pointer dark:text-green-400 dark:hover:text-green-300">
            Forgot Password?
          </button>
        </div>
        <button type="submit"
                class="w-full bg-green-600 text-white py-3 rounded-lg hover:bg-green-700 transition-colors !rounded-button whitespace-nowrap cursor-pointer">
          {{ showOtpField ? 'Verify OTP' : 'Login' }}
        </button>
      </form>
      <div class="mt-6 text-center">
        <p class="text-gray-600 dark:text-gray-400">Don't have an account?</p>
        <button @click="$emit('register-click')"
                class="text-green-600 hover:text-green-800 font-medium cursor-pointer dark:text-green-400 dark:hover:text-green-300">
          Register Now
        </button>
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, computed, onUnmounted } from "vue";

  const emit = defineEmits(['login-success', 'register-click']);

  const loginForm = ref({
    email: "",
    password: "",
    otp: "",
    rememberMe: false,
  });

  const showPassword = ref(false);
  const showOtpField = ref(false);

  // OTP Timer State
  const otpTimer = ref(0); // seconds
  const canResendOtp = ref(false);
  let timerInterval: number | null = null;

  const startOtpTimer = () => {
    if (timerInterval) {
      clearInterval(timerInterval);
    }
    otpTimer.value = 180; // 3 minutes
    canResendOtp.value = false;

    timerInterval = setInterval(() => {
      if (otpTimer.value > 0) {
        otpTimer.value--;
      } else {
        clearInterval(timerInterval!);
        canResendOtp.value = true;
        timerInterval = null;
      }
    }, 1000) as unknown as number; // Type assertion for setInterval return type
  };

  const formatTime = computed(() => {
    const minutes = Math.floor(otpTimer.value / 60);
    const seconds = otpTimer.value % 60;
    return `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')}`;
  });

  const resendOtp = () => {
    // In a real application, you would make an API call here to request a new OTP
    console.log("Resending OTP for:", loginForm.value.email);
    startOtpTimer(); // Restart the timer
  };

  const handleLogin = () => {
    if (!showOtpField.value) {
      showOtpField.value = true;
      startOtpTimer(); // Start the timer when OTP field is shown
      // In a real app, you'd send email/password to backend and request OTP
      console.log("Logging in and requesting OTP:", loginForm.value.email);
    } else {
      // In a real app, you'd send OTP to backend for verification
      console.log("Verifying OTP:", loginForm.value.otp);
      // Simulate successful login
      emit('login-success');
      // Clear interval if user logs in before timer finishes
      if (timerInterval) {
        clearInterval(timerInterval);
        timerInterval = null;
      }
      // Reset state
      showOtpField.value = false;
      loginForm.value.otp = "";
      otpTimer.value = 0;
      canResendOtp.value = false;
    }
  };

  // Clean up the interval when the component is unmounted
  onUnmounted(() => {
    if (timerInterval) {
      clearInterval(timerInterval);
    }
  });
</script>
