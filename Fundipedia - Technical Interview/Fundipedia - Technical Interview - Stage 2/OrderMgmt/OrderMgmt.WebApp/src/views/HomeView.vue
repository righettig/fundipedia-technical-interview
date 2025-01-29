<script setup lang="ts">
import { ref } from "vue";

const increaseCount = () => {
  count.value++;

  if (count.value === 3) {
    count.value = 0;
  }
};

const count = ref(0);
</script>

<template>
  <Button @click="increaseCount" label="Count"></Button>
  <h5>{{ count }}</h5>
</template>
