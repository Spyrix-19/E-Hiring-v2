
  // src/main.ts
  import { createApp } from 'vue';
  import App from './App.vue';


  createApp(App).mount('#app');

  // PWA Service Worker Registration (Optional, but good for PWA)
  if ('serviceWorker' in navigator) {
    window.addEventListener('load', () => {
      navigator.serviceWorker.register('/service-worker.js')
        .then(registration => {
          console.log('SW registered: ', registration);
        })
        .catch(registrationError => {
          console.log('SW registration failed: ', registrationError);
        });
    });
  }
