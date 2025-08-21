<template>
  <div class="h-screen w-64 bg-gray-800 text-white flex flex-col">
    <!-- Logo -->
    <div class="p-4 text-2xl font-bold border-b border-gray-700">
      🎨 Painting Manager
    </div>

    <!-- Links -->
    <nav class="flex-1 p-2 space-y-2">
      <RouterLink
        v-for="item in menuItems"
        :key="item.to"
        :to="item.to"
        class="block px-3 py-2 rounded-lg hover:bg-gray-700"
        active-class="bg-gray-700 font-semibold"
      >
        {{ item.label }}
      </RouterLink>
    </nav>

    <!-- Logout -->
    <button
      class="p-3 bg-red-600 hover:bg-red-700 rounded-lg m-2"
      @click="logout"
    >
      Logout
    </button>
  </div>
</template>

<script setup>
import { useAuthStore } from "../stores/auth";
import { useRouter } from "vue-router";

const auth = useAuthStore();
const router = useRouter();

const menuItems = [
  { to: "/users", label: "Users" },
  { to: "/roles", label: "Roles" },
  { to: "/groups", label: "Groups" },
  { to: "/colors", label: "Colors" },
  { to: "/components", label: "Components" },
  { to: "/formulas", label: "Formulas" },
];

function logout() {
  auth.logout();
  router.push("/login");
}
</script>
