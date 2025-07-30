<template>
  <div class="min-h-screen bg-gray-50 dark:bg-gray-900 text-gray-900 dark:text-gray-100">
    <Login v-if="currentView === 'login'" @login-success="handleLoginSuccess" @register-click="currentView = 'register'" />

    <Register v-if="currentView === 'register'" @register-success="handleRegisterSuccess" @login-click="currentView = 'login'" />

    <div v-if="currentView === 'dashboard'" class="flex h-screen bg-gray-50 dark:bg-gray-900">
      <div class="w-64 bg-white dark:bg-gray-800 shadow-lg hidden md:block flex-shrink-0 border-r dark:border-gray-700">
        <div class="p-6 border-b dark:border-gray-700">
          <div class="flex items-center space-x-3">
            <div class="w-10 h-10 bg-green-600 rounded-full flex items-center justify-center dark:bg-green-700">
              <i class="fas fa-shield-alt text-white"></i>
            </div>
            <div>
              <h2 class="font-bold text-gray-800 dark:text-gray-200">BFP Hiring</h2>
              <p class="text-xs text-gray-600 dark:text-gray-400">Electronic System</p>
            </div>
          </div>
        </div>
        <nav class="mt-6">
          <div class="px-4 space-y-2">
            <button @click="activeSection = 'dashboard'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'dashboard' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-home w-5"></i>
              <span class="ml-3">Dashboard</span>
            </button>
            <button @click="activeSection = 'profile'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'profile' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-user w-5"></i>
              <span class="ml-3">Profile</span>
            </button>
            <button @click="activeSection = 'documents'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'documents' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-file-alt w-5"></i>
              <span class="ml-3">Documents</span>
            </button>
            <button @click="activeSection = 'medical'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'medical' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-heartbeat w-5"></i>
              <span class="ml-3">Medical Documents</span>
            </button>
            <button @click="activeSection = 'calendar'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'calendar' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-calendar-alt w-5"></i>
              <span class="ml-3">Calendars</span>
            </button>
            <button @click="activeSection = 'tracker'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'tracker' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-tasks w-5"></i>
              <span class="ml-3">Application Tracker</span>
            </button>
            <button @click="activeSection = 'esumbong'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'esumbong' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-exclamation-triangle w-5"></i>
              <span class="ml-3">E-Sumbong</span>
            </button>
            <button @click="activeSection = 'settings'"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'settings' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-cog w-5"></i>
              <span class="ml-3">Settings</span>
            </button>
            <div class="absolute bottom-4 left-4 right-4 px-4 space-y-2">
              <button @click="handleLogout"
                      class="w-full flex items-center px-4 py-3 text-red-600 hover:bg-red-50 rounded-lg transition-colors cursor-pointer dark:text-red-400 dark:hover:bg-red-900/20">
                <i class="fas fa-sign-out-alt w-5"></i>
                <span class="ml-3">Logout</span>
              </button>
            </div>
          </div>
          <!--<div class="absolute bottom-4 left-4 right-4 px-4 space-y-2">
            <button @click="handleLogout"
                    class="w-full flex items-center px-4 py-3 text-red-600 hover:bg-red-50 rounded-lg transition-colors cursor-pointer dark:text-red-400 dark:hover:bg-red-900/20">
              <i class="fas fa-sign-out-alt w-5"></i>
              <span class="ml-3">Logout</span>
            </button>
          </div>-->
        </nav>
      </div>

      <div v-if="isSidebarOpen"
           @click="toggleSidebar"
           class="fixed inset-0 bg-black opacity-50 z-40 md:hidden dark:opacity-70"></div>

      <div :class="['sidebar bg-white dark:bg-gray-800 shadow-lg md:hidden border-r dark:border-gray-700', { 'sidebar-mobile-open': isSidebarOpen, 'sidebar-mobile-closed': !isSidebarOpen }]">
        <div class="p-6 border-b dark:border-gray-700">
          <div class="flex items-center justify-between">
            <div class="flex items-center space-x-3">
              <div class="w-10 h-10 bg-green-600 rounded-full flex items-center justify-center dark:bg-green-700">
                <i class="fas fa-shield-alt text-white"></i>
              </div>
              <div>
                <h2 class="font-bold text-gray-800 dark:text-gray-200">BFP Hiring</h2>
                <p class="text-xs text-gray-600 dark:text-gray-400">Electronic System</p>
              </div>
            </div>
            <button @click="toggleSidebar" class="text-gray-600 text-xl dark:text-gray-400">
              <i class="fas fa-times"></i>
            </button>
          </div>
        </div>
        <nav class="mt-6">
          <div class="px-4 space-y-2">
            <button @click="activeSection = 'dashboard'; toggleSidebar()"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'dashboard' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-home w-5"></i>
              <span class="ml-3">Dashboard</span>
            </button>
            <button @click="activeSection = 'profile'; toggleSidebar()"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'profile' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-user w-5"></i>
              <span class="ml-3">Profile</span>
            </button>
            <button @click="activeSection = 'documents'; toggleSidebar()"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'documents' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-file-alt w-5"></i>
              <span class="ml-3">Documents</span>
            </button>
            <button @click="activeSection = 'medical'; toggleSidebar()"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'medical' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-heartbeat w-5"></i>
              <span class="ml-3">Medical Documents</span>
            </button>
            <button @click="activeSection = 'tracker'; toggleSidebar()"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'tracker' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-tasks w-5"></i>
              <span class="ml-3">Application Tracker</span>
            </button>
            <button @click="activeSection = 'esumbong'; toggleSidebar()"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'esumbong' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-exclamation-triangle w-5"></i>
              <span class="ml-3">E-Sumbong</span>
            </button>
            <button @click="activeSection = 'settings'; toggleSidebar()"
                    :class="{ 'bg-green-50 text-green-600 border-r-2 border-green-600 dark:bg-green-900/20 dark:text-green-400 dark:border-green-400': activeSection === 'settings' }"
                    class="w-full flex items-center px-4 py-3 text-left text-gray-700 hover:bg-gray-50 rounded-lg transition-colors cursor-pointer
                           dark:text-gray-300 dark:hover:bg-gray-700">
              <i class="fas fa-cog w-5"></i>
              <span class="ml-3">Settings</span>
            </button>
          </div>
          <div class="absolute bottom-4 left-4 right-4 px-4 space-y-2">
            <button @click="handleLogout"
                    class="w-full flex items-center px-4 py-3 text-red-600 hover:bg-red-50 rounded-lg transition-colors cursor-pointer
                  dark:text-red-400 dark:hover:bg-red-900/20">
              <i class="fas fa-sign-out-alt w-5"></i>
              <span class="ml-3">Logout</span>
            </button>
          </div>
        </nav>
      </div>

      <div :class="['flex-1 overflow-auto', { 'main-content-desktop-shifted': !isSidebarOpen && windowWidth > 768, 'main-content-shifted': isSidebarOpen && windowWidth <= 768 }]">
        <header class="bg-white dark:bg-gray-800 shadow-sm border-b dark:border-gray-700 px-6 py-4 flex items-center justify-between">
          <button @click="toggleSidebar" class="md:hidden text-gray-600 text-xl dark:text-gray-400 mr-4">
            <i class="fas fa-bars"></i>
          </button>
          <div>
            <h1 class="text-2xl font-bold text-gray-800 dark:text-gray-200">
              {{ getSectionTitle() }}
            </h1>
            <p class="text-gray-600 dark:text-gray-400">{{ getCurrentDate() }}</p>
          </div>
          <div class="flex items-center space-x-4">
            <div class="relative notifications-container">
              <button @click="toggleNotifications"
                      class="relative p-2 text-gray-600 hover:text-gray-800 cursor-pointer notifications-button
                             dark:text-gray-400 dark:hover:text-gray-200">
                <i class="fas fa-bell text-xl"></i>
                <span v-if="unreadNotifications.length > 0"
                      class="absolute -top-1 -right-1 w-3 h-3 bg-red-500 rounded-full"></span>
              </button>
              <div v-if="showNotifications"
                   class="absolute right-0 mt-2 w-96 bg-white dark:bg-gray-700 rounded-xl shadow-lg border border-gray-200 dark:border-gray-600 z-50 notifications-dropdown">
                <div class="p-4 border-b border-gray-200 dark:border-gray-600">
                  <div class="flex items-center justify-between">
                    <h3 class="font-semibold text-gray-800 dark:text-gray-200">Notifications</h3>
                    <button @click="markAllAsRead"
                            class="text-sm text-green-600 hover:text-green-800 dark:text-green-400 dark:hover:text-green-300">
                      Mark all as read
                    </button>
                  </div>
                </div>
                <div class="max-h-96 overflow-y-auto">
                  <div v-if="notifications.length === 0"
                       class="p-4 text-center text-gray-600 dark:text-gray-400">
                    No notifications
                  </div>
                  <div v-for="notification in notifications"
                       :key="notification.id"
                       class="p-4 border-b border-gray-100 dark:border-gray-600 hover:bg-gray-50 dark:hover:bg-gray-600 transition-colors"
                       :class="{ 'bg-green-50 dark:bg-green-900/10': !notification.read }">
                    <div class="flex items-start justify-between">
                      <div class="flex-1">
                        <p class="font-medium text-gray-800 dark:text-gray-200 mb-1">
                          {{ notification.title }}
                        </p>
                        <p class="text-sm text-gray-600 dark:text-gray-400 mb-2">
                          {{ notification.content }}
                        </p>
                        <div class="flex items-center text-xs text-gray-500 dark:text-gray-400">
                          <i class="fas fa-clock mr-1"></i>
                          {{ notification.timestamp }}
                        </div>
                      </div>
                      <div class="flex items-center space-x-2 ml-4">
                        <button v-if="!notification.read"
                                @click="markAsRead(notification.id)"
                                class="text-green-600 hover:text-green-800 dark:text-green-400 dark:hover:text-green-300">
                          <i class="fas fa-check"></i>
                        </button>
                        <button @click="dismissNotification(notification.id)"
                                class="text-gray-400 hover:text-gray-600 dark:text-gray-500 dark:hover:text-gray-400">
                          <i class="fas fa-times"></i>
                        </button>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flex items-center space-x-3">
              <div class="w-8 h-8 bg-green-600 rounded-full flex items-center justify-center dark:bg-green-700">
                <i class="fas fa-user text-white text-sm"></i>
              </div>
              <span class="text-gray-700 font-medium dark:text-gray-300">{{ userProfile.firstName }} {{ userProfile.lastName }}</span>
            </div>
          </div>
        </header>

        <Dashboard v-if="activeSection === 'dashboard'"
                   :user-profile="userProfile"
                   :application-progress="applicationProgress"
                   :documents-uploaded="documentsUploaded"
                   :total-documents="totalDocuments"
                   :quick-links="quickLinks"
                   :announcements="announcements"
                   @change-section="activeSection = $event" />
        <Profile v-if="activeSection === 'profile'"
                 :user-profile="userProfile"
                 @update-profile="updateUserProfile" />
        <Documents v-if="activeSection === 'documents'"
                   :required-documents="requiredDocuments"
                   @document-uploaded="handleDocumentUpload" />
        <MedicalDocuments v-if="activeSection === 'medical'"
                          :medical-documents="medicalDocuments"
                          @medical-document-uploaded="handleMedicalDocumentUpload" />
        <ApplicationTracker v-if="activeSection === 'tracker'"
                            :current-stage="currentStage"
                            :application-stages="applicationStages" />
        <Calendar v-if="activeSection === 'calendar'" :events="calendarEvents" />
        <ESumbong v-if="activeSection === 'esumbong'"
                  :complaint-history="complaintHistory"
                  @submit-complaint="addComplaint" />
        <Settings v-if="activeSection === 'settings'"
                  :settings="settings"
                  @update-email="updateSettingsEmail"
                  @update-password="updateSettingsPassword"
                  @update-theme="updateSettingsTheme"
                  @update-language="updateSettingsLanguage"
                  @open-support="openSupport"
                  @open-survey="openSurvey" />
      </div>
    </div>
  </div>
</template>

<script lang="ts" setup>
  import { ref, computed, onMounted, watch, onUnmounted } from "vue";
  import Login from "./components/Login.vue";
  import Register from "./components/Register.vue";
  import Dashboard from "./components/Dashboard.vue";
  import Profile from "./components/Profile.vue";
  import Documents from "./components/Documents.vue";
  import MedicalDocuments from "./components/MedicalDocuments.vue";
  import Calendar from "./components/Calendar.vue";
  import ApplicationTracker from "./components/ApplicationTracker.vue";
  import ESumbong from "./components/ESumbong.vue";
  import Settings from "./components/Settings.vue";

  // Reactive data
  const currentView = ref("login"); // Controls which main screen is shown
  const activeSection = ref("dashboard"); // Controls which dashboard section is shown
  const showNotifications = ref(false);
  const isSidebarOpen = ref(false); // State for mobile sidebar

  // Reactive window width for responsive logic
  const windowWidth = ref(typeof window !== 'undefined' ? window.innerWidth : 0);

  // Global data (could be fetched from an API in a real app)
  const userProfile = ref({
    firstName: "Juan",
    middleName: "Santos",
    lastName: "Dela Cruz",
    suffix: "Jr",
    birthday: "1995-06-15",
    age: 28,
    permanentaddress: {
      street: "123 Main Street",
      barangay: "Barangay San Jose",
      city: "Manila",
      province: "Metro Manila",
    },
    presentaddress: {
      street: "123 Main Street",
      barangay: "Barangay San Jose",
      city: "Manila",
      province: "Metro Manila",
    },
  });

  const notifications = ref([
    {
      id: 1,
      title: "Document Approved",
      content: "Your Application Letter has been approved by the reviewing officer.",
      timestamp: "2 hours ago",
      read: false,
      type: "document",
    },
    {
      id: 2,
      title: "New Announcement",
      content: "Important update regarding the medical examination schedule.",
      timestamp: "5 hours ago",
      read: false,
      type: "announcement",
    },
    {
      id: 3,
      title: "Application Status Update",
      content: "You have successfully completed the Physical Ability Test (PAT).",
      timestamp: "1 day ago",
      read: true,
      type: "status",
    },
    {
      id: 4,
      title: "System Maintenance",
      content: "The system will undergo maintenance on March 25, 2024.",
      timestamp: "2 days ago",
      read: true,
      type: "system",
    },
  ]);

  // Add this new data for calendar events
  const calendarEvents = ref([
    { date: new Date(2024, 2, 10), title: "QWE (Qualifying Written Exam)" }, // March 10, 2024 (example from application stages)
    { date: new Date(2024, 2, 15), title: "PAT (Physical Ability Test)" }, // March 15, 2024 (example from application stages)
    { date: new Date(2024, 2, 20), title: "Panel Interview" }, // March 20, 2024 (example from application stages)
    { date: new Date(2024, 3, 1), title: "Medical Examination Start" }, // April 1, 2024 (example from announcements)
    { date: new Date(2024, 3, 15), title: "Medical Examination End" }, // April 15, 2024 (example from announcements)
    { date: new Date(2024, 2, 31), title: "Document Submission Deadline Extension" }, // March 31, 2024 (example from announcements)
    { date: new Date(2024, 2, 20), title: "System Maintenance (2:00 AM - 6:00 AM)" }, // March 20, 2024 (example from announcements)
  ]);

  const applicationProgress = ref(65);
  const documentsUploaded = ref(12);
  const totalDocuments = ref(15);
  const currentStage = ref(4);

  const quickLinks = ref([
    {
      id: 1,
      title: "Upload Documents",
      description: "Submit required documents",
      icon: "fas fa-upload",
      bgColor: "bg-blue-500",
      section: "documents",
    },
    {
      id: 2,
      title: "Track Application",
      description: "Monitor your progress",
      icon: "fas fa-chart-line",
      bgColor: "bg-green-500",
      section: "tracker",
    },
    {
      id: 3,
      title: "Medical Records",
      description: "Upload medical documents",
      icon: "fas fa-heartbeat",
      bgColor: "bg-red-500",
      section: "medical",
    },
    {
      id: 4,
      title: "Profile Settings",
      description: "Update personal info",
      icon: "fas fa-user-cog",
      bgColor: "bg-purple-500",
      section: "profile",
    },
  ]);

  const announcements = ref([
    {
      id: 1,
      title: "Application Deadline Extended",
      content:
        "The deadline for document submission has been extended to March 31, 2024.",
      date: "2024-03-15",
      priority: "high",
    },
    {
      id: 2,
      title: "Medical Examination Schedule",
      content: "Medical examinations will be conducted from April 1-15, 2024.",
      date: "2024-03-10",
      priority: "medium",
    },
    {
      id: 3,
      title: "System Maintenance Notice",
      content:
        "The system will be under maintenance on March 20, 2024 from 2:00 AM to 6:00 AM.",
      date: "2024-03-08",
      priority: "low",
    },
  ]);

  const requiredDocuments = ref([
    {
      id: 1,
      name: "Application Letter",
      description: "Formal application letter",
      icon: "fa-file-alt",
      status: "approved",
      statusDate: "2024-03-15",
      required: true,
      uploaded: true, // Ensure consistency with status
    },
    {
      id: 2,
      name: "Personal Data Sheet (PDS)",
      description: "Complete PDS form",
      icon: "fa-file-user",
      status: "processing",
      statusDate: "2024-03-20",
      required: true,
      uploaded: true, // Ensure consistency with status
    },
    {
      id: 3,
      name: "Eligibility Certificate",
      description: "Civil service eligibility",
      icon: "fa-certificate",
      status: "uploaded",
      statusDate: "2024-03-22",
      required: true,
      uploaded: true, // Ensure consistency with status
    },
    {
      id: 4,
      name: "Transcript of Records (TOR)",
      description: "Official academic transcript",
      icon: "fa-graduation-cap",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 5,
      name: "Diploma",
      description: "College diploma or certificate",
      icon: "fa-scroll",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 6,
      name: "Birth Certificate",
      description: "NSO/PSA birth certificate",
      icon: "fa-baby",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 7,
      name: "Marriage Certificate",
      description: "NSO/PSA marriage certificate",
      icon: "fa-ring",
      uploaded: false,
      required: false,
      status: "pending",
      statusDate: null,
    },
    {
      id: 8,
      name: "Barangay Clearance",
      description: "Certificate from barangay",
      icon: "fa-home",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 9,
      name: "Mayor's Clearance",
      description: "Certificate from mayor's office",
      icon: "fa-city",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 10,
      name: "MTC Clearance",
      description: "Municipal trial court clearance",
      icon: "fa-gavel",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 11,
      name: "RTC Clearance",
      description: "Regional trial court clearance",
      icon: "fa-balance-scale",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 12,
      name: "Police Clearance",
      description: "PNP clearance certificate",
      icon: "fa-shield-alt",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 13,
      name: "NBI Clearance",
      description: "National Bureau of Investigation clearance",
      icon: "fa-search",
      uploaded: true,
      required: true,
      status: "uploaded",
      statusDate: "2024-03-22",
    },
    {
      id: 14,
      name: "Age Waiver",
      description: "Age requirement waiver (if applicable)",
      icon: "fa-calendar-times",
      uploaded: false,
      required: false,
      status: "pending",
      statusDate: null,
    },
    {
      id: 15,
      name: "Height Waiver",
      description: "Height requirement waiver (if applicable)",
      icon: "fa-ruler-vertical",
      uploaded: false,
      required: false,
      status: "pending",
      statusDate: null,
    },
  ]);

  const medicalDocuments = ref([
    {
      id: 1,
      name: "ECG (Electrocardiogram)",
      description: "Heart rhythm and electrical activity test for initial screening",
      icon: "fa-heartbeat",
      uploaded: true,
    },
    {
      id: 2,
      name: "Serum Pregnancy Test",
      description: "Blood test to detect pregnancy for initial screening (female applicants)",
      icon: "fa-vial",
      uploaded: false,
    },
  ]);

  const applicationStages = ref([
    {
      id: 1,
      name: "Initial Screening",
      description: "Document verification and basic requirements check",
      date: "2024-03-01",
      status: "completed",
      note: "All documents verified successfully",
    },
    {
      id: 2,
      name: "QWE (Qualifying Written Exam)",
      description: "Written examination for qualification assessment",
      date: "2024-03-10",
      status: "completed",
      note: "Passed with score: 89/100",
    },
    {
      id: 3,
      name: "PAT (Physical Ability Test)",
      description: "Physical fitness and ability evaluation",
      date: "2024-03-15",
      status: "completed",
      note: "Successfully completed all physical tests",
    },
    {
      id: 4,
      name: "Panel Interview",
      description: "Face-to-face interview with selection panel",
      date: "2024-03-20",
      status: "in_progress",
      note: "Scheduled for final round",
    },
    {
      id: 5,
      name: "Complete Background Investigation (CBI)",
      description: "Thorough background and character investigation",
      date: null,
      status: "pending",
      note: null,
    },
    {
      id: 6,
      name: "NPE & DT (Neuro-Psychiatric Exam & Drug Test)",
      description: "Mental health evaluation and drug screening",
      date: null,
      status: "pending",
      note: null,
    },
    {
      id: 7,
      name: "Medical, Dental & Physical Examination",
      description: "Comprehensive health assessment",
      date: null,
      status: "pending",
      note: null,
    },
    {
      id: 8,
      name: "Final Deliberation",
      description: "Final evaluation and decision making",
      date: null,
      status: "pending",
      note: null,
    },
    {
      id: 9,
      name: "Hired",
      description: "Successful completion and job offer",
      date: null,
      status: "pending",
      note: null,
    },
  ]);

  const complaintHistory = ref([
    {
      id: 1,
      subject: "Document Upload Issue",
      description: "Unable to upload birth certificate due to file size limit",
      category: "Technical",
      status: "Resolved",
      date: "2024-03-10",
    },
    {
      id: 2,
      subject: "Examination Schedule Conflict",
      description: "Requesting to reschedule PAT due to medical emergency",
      category: "Process",
      status: "In Progress",
      date: "2024-03-12",
    },
  ]);

  const settings = ref({
    email: "user@example.com",
    currentPassword: "",
    newPassword: "",
    confirmPassword: "",
    theme: "light", // Default theme
    language: "en",
  });

  // Computed properties
  const unreadNotifications = computed(() => {
    return notifications.value.filter((notification) => !notification.read);
  });

  const getSectionTitle = () => {
    const titles: { [key: string]: string } = {
      dashboard: "Dashboard",
      profile: "Profile",
      documents: "Documents",
      medical: "Medical Documents",
      tracker: "Application Tracker",
      calendar: "Calendar of Activities",
      esumbong: "E-Sumbong",
      settings: "Settings",
    };
    return titles[activeSection.value] || "Dashboard";
  };

  const getCurrentDate = () => {
    return new Date().toLocaleDateString("en-US", {
      weekday: "long",
      year: "numeric",
      month: "long",
      day: "numeric",
    });
  };

  // Methods for main App.vue
  const handleLoginSuccess = () => {
    currentView.value = "dashboard";
  };

  const handleRegisterSuccess = () => {
    currentView.value = "login"; // After registration, go back to login
  };

  const handleLogout = () => {
    currentView.value = "login";
    // Reset any user-specific data if necessary
    // Example: localStorage.removeItem('authToken');
  };

  const toggleNotifications = () => {
    showNotifications.value = !showNotifications.value;
  };

  const markAsRead = (id: number) => {
    const notification = notifications.value.find((n) => n.id === id);
    if (notification) {
      notification.read = true;
    }
  };

  const markAllAsRead = () => {
    notifications.value.forEach((notification) => {
      notification.read = true;
    });
  };

  const dismissNotification = (id: number) => {
    notifications.value = notifications.value.filter((n) => n.id !== id);
  };

  // Handle window resize for responsiveness
  const handleResize = () => {
    windowWidth.value = window.innerWidth;
  };

  // Close notifications and manage sidebar on component mount and unmount
  onMounted(() => {
    // Initialize theme based on localStorage or system preference
    const savedTheme = localStorage.getItem("theme");
    if (savedTheme) {
      settings.value.theme = savedTheme;
      document.documentElement.classList.toggle('dark', savedTheme === "dark");
    } else if (window.matchMedia('(prefers-color-scheme: dark)').matches) {
      settings.value.theme = "dark";
      document.documentElement.classList.add('dark');
    } else {
      settings.value.theme = "light";
      document.documentElement.classList.remove('dark');
    }


    // Close notifications when clicking outside
    document.addEventListener("click", (event: MouseEvent) => {
      const target = event.target as HTMLElement;
      const notificationsContainer = document.querySelector(".notifications-container");
      if (notificationsContainer && !notificationsContainer.contains(target)) {
        showNotifications.value = false;
      }
    });

    // Add resize listener
    if (typeof window !== 'undefined') {
      window.addEventListener('resize', handleResize);
      handleResize(); // Initial set
    }
  });

  onUnmounted(() => {
    // Remove resize listener to prevent memory leaks
    if (typeof window !== 'undefined') {
      window.removeEventListener('resize', handleResize);
    }
  });

  // Function to toggle sidebar for mobile
  const toggleSidebar = () => {
    isSidebarOpen.value = !isSidebarOpen.value;
  };

  // Watch for activeSection changes to close sidebar on mobile
  watch(activeSection, () => {
    if (windowWidth.value <= 768) { // Use reactive windowWidth
      isSidebarOpen.value = false;
    }
  });

  // Methods to update global data from child components (passed as props/emits)
  const updateUserProfile = (updatedProfile: typeof userProfile.value) => {
    userProfile.value = { ...userProfile.value, ...updatedProfile };
    alert("Profile updated successfully!"); // Replace with a custom modal/toast
  };

  const handleDocumentUpload = (docId: number) => {
    const doc = requiredDocuments.value.find((d) => d.id === docId);
    if (doc) {
      doc.status = "uploaded";
      doc.statusDate = new Date().toISOString().split("T")[0];
      if (!doc.uploaded) {
        documentsUploaded.value++;
      }
      doc.uploaded = true;
    }
  };

  const handleMedicalDocumentUpload = (docId: number) => {
    const doc = medicalDocuments.value.find((d) => d.id === docId);
    if (doc) {
      doc.uploaded = true;
    }
  };

  const addComplaint = (newComplaintData: typeof complaintHistory.value[0]) => {
    const newComplaint = {
      id: complaintHistory.value.length + 1,
      ...newComplaintData,
      status: "Submitted",
      date: new Date().toISOString().split("T")[0],
    };
    complaintHistory.value.unshift(newComplaint);
    alert("Complaint submitted successfully!"); // Replace with a custom modal/toast
  };

  const updateSettingsEmail = (email: string) => {
    settings.value.email = email;
    alert("Email update request sent. Please check your inbox for verification."); // Replace with a custom modal/toast
  };

  const updateSettingsPassword = (newPass: string, confirmPass: string) => {
    if (newPass !== confirmPass) {
      alert("New passwords do not match!"); // Replace with a custom modal/toast
      return;
    }
    settings.value.newPassword = newPass; // In a real app, you'd hash and send to backend
    settings.value.confirmPassword = confirmPass;
    alert("Password updated successfully!"); // Replace with a custom modal/toast
  };

  // 🌓 Theme toggle function
  const updateSettingsTheme = (theme: string) => {
    settings.value.theme = theme;
    document.documentElement.classList.toggle('dark', theme === 'dark');
    localStorage.setItem('theme', theme);
  };

  const updateSettingsLanguage = (language: string) => {
    settings.value.language = language;
    // Implement i18n logic here
  };

  const openSupport = () => {
    alert("Support center will be opened"); // Replace with a custom modal/toast
  };

  const openSurvey = () => {
    alert("Survey form will be opened"); // Replace with a custom modal/toast
  };
</script>

