<template>
  <div class="relative">
    <input type="text"
           :value="selectedText"
           @input="handleInput"
           @focus="showDropdown = true"
           @blur="hideDropdown"
           :placeholder="placeholder"
           class="w-full px-4 py-3 border border-gray-300 dark:border-gray-600 rounded-lg focus:ring-2 focus:ring-green-500 focus:border-transparent bg-white dark:bg-gray-700 text-gray-900 dark:text-gray-100 placeholder-gray-400 dark:placeholder-gray-500"
           :disabled="disabled" />
    <ul v-if="showDropdown && filteredOptions.length > 0" class="absolute z-10 w-full bg-white dark:bg-gray-700 border border-gray-300 dark:border-gray-600 rounded-lg mt-1 max-h-60 overflow-y-auto shadow-lg">
      <li v-for="option in filteredOptions"
          :key="option.value"
          @mousedown.prevent="selectOption(option)"
          class="px-4 py-2 cursor-pointer hover:bg-gray-100 dark:hover:bg-gray-600 text-gray-900 dark:text-gray-100">
        {{ option.text }}
      </li>
    </ul>
    <p v-if="showDropdown && filteredOptions.length === 0 && searchText" class="px-4 py-2 text-gray-500 dark:text-gray-400">No results found.</p>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref, computed, watch } from 'vue';

export default defineComponent({
  props: {
    options: {
      type: Array as () => { text: string; value: string }[],
      required: true,
    },
    modelValue: {
      type: String,
      default: '',
    },
    placeholder: {
      type: String,
      default: '',
    },
    disabled: {
      type: Boolean,
      default: false,
    }
  },
  emits: ['update:modelValue', 'change'],
  setup(props, { emit }) {
    const searchText = ref('');
    const showDropdown = ref(false);

    // Find the text for the currently selected value
    const selectedText = computed(() => {
      const selected = props.options.find(option => option.value === props.modelValue);
      return selected ? selected.text : searchText.value;
    });

    const filteredOptions = computed(() => {
      if (!searchText.value) {
        return props.options;
      }
      const lowerSearchText = searchText.value.toLowerCase();
      return props.options.filter(option =>
        option.text.toLowerCase().includes(lowerSearchText)
      );
    });

    const handleInput = (event: Event) => {
      searchText.value = (event.target as HTMLInputElement).value;
      showDropdown.value = true;
      // Emit update:modelValue with the current input text (or clear if no match)
      const matchedOption = props.options.find(option => option.text.toLowerCase() === searchText.value.toLowerCase());
      if (matchedOption) {
        emit('update:modelValue', matchedOption.value);
      } else {
        emit('update:modelValue', ''); // Clear modelValue if no exact match
      }
    };

    const selectOption = (option: { text: string; value: string }) => {
      emit('update:modelValue', option.value);
      emit('change', option.value); // Custom change event
      searchText.value = option.text; // Update searchText to reflect selected text
      showDropdown.value = false;
    };

    const hideDropdown = () => {
      setTimeout(() => { // Small delay to allow click event on option to register
        showDropdown.value = false;
        // If the selectedText doesn't match a current option, clear the input text
        const matchedOption = props.options.find(option => option.value === props.modelValue);
        if (matchedOption) {
          searchText.value = matchedOption.text;
        } else {
          searchText.value = ''; // Clear if no valid selection after blur
          emit('update:modelValue', ''); // Also clear modelValue if input doesn't match an option
        }
      }, 100);
    };

    watch(() => props.modelValue, (newVal) => {
      const selected = props.options.find(option => option.value === newVal);
      searchText.value = selected ? selected.text : '';
    }, { immediate: true });

    return {
      searchText,
      showDropdown,
      filteredOptions,
      handleInput,
      selectOption,
      hideDropdown,
      selectedText
    };
  },
});
</script>

<style scoped>
  /* Add any specific styles for your dropdown here if not fully covered by Tailwind */
</style>
