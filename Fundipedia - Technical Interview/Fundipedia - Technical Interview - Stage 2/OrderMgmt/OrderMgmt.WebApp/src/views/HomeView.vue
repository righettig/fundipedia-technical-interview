<script setup lang="ts">
import { ref, reactive } from 'vue';
import axios from 'axios';

const state = reactive(
  {
    order: {
      orderType: { name: 'Repair', code: 0 },
      isRushOrder: false,
      isNewCustomer: false,
      isLargeOrder: false,
    },
    orderTypes: [
      { name: 'Repair', code: 0 },
      { name: 'Hire', code: 1 },
    ],
    orderStatus: [
      { name: 'Confirmed', code: 0 },
      { name: 'Closed', code: 1 },
      { name: 'AuthorisationRequired', code: 2 },
    ]
  }
);

const apiOrderStatus = ref(null);

// Handle form submission
const submitForm = async () => {
  console.log('Form Data:', state.order);

  // extract orderType code from UI model
  const request = {
    ...state.order,
    orderType: state.order.orderType.code
  }

  try {
    const response = await axios.post('http://localhost:5182/orders/process', request);

    // map from API DTO object to UI model
    const orderStatus = state.orderStatus.find(type => type.code === response.data) || 'Unknown';
    apiOrderStatus.value = orderStatus.name;

  } catch (error) {
    apiOrderStatus.value = 'Error processing the order';
  }
};
</script>

<template>
  <div class="home">
    <h1>Order Processing</h1>

    <!-- Order Form -->
    <form @submit.prevent="processOrder">
      <div class="form-group">
        <label for="orderType">Order Type</label>
        <Select v-model="state.order.orderType" :options="state.orderTypes" optionLabel="name" />
      </div>

      <div class="form-group">
        <label for="isRushOrder">Is Rush Order</label>
        <Checkbox v-model="state.order.isRushOrder" binary />
      </div>

      <div class="form-group">
        <label for="isNewCustomer">Is New Customer</label>
        <Checkbox v-model="state.order.isNewCustomer" binary />
      </div>

      <div class="form-group">
        <label for="isLargeOrder">Is Large Order</label>
        <Checkbox v-model="state.order.isLargeOrder" binary />
      </div>

      <Button type="submit" @click="submitForm" label="Process Order" icon="pi pi-check"></Button>
    </form>

    <!-- Result Section -->
    <div v-if="apiOrderStatus" class="result">
      <h3>Order Status</h3>
      <p>{{ apiOrderStatus }}</p>
    </div>
  </div>
</template>

<style scoped>
.home {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
}

.form-group {
  margin-bottom: 20px;
}

.result {
  margin-top: 20px;
  padding: 10px;
  border: 1px solid #ccc;
  background-color: #f9f9f9;
}

h1 {
  text-align: center;
}
</style>
