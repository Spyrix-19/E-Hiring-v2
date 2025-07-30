// tailwind.config.js (conceptual)
module.exports = {
  darkMode: 'class', // This enables dark mode based on the 'dark' class on the html tag
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}
