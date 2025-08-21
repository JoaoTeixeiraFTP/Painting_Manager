<template>
  <div class="flex">
    <!-- Side menu fixo -->
    <SideMenu />

    <!-- Conteúdo principal -->
    <div class="flex-1 p-6 bg-gray-100 min-h-screen">
      <router-view />
    </div>
  </div>
</template>

<script setup>
import SideMenu from "../components/SideMenu.vue";
</script>
