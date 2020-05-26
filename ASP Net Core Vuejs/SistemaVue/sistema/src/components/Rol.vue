<template>
  <v-layout align-start>
    <v-flex>
      <!-- Tabla CRUD -->
      <v-data-table
        :headers="headers"
        :items="roles"
        :search="search"
        sort-by="nombre"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar flat color="white">
            <v-toolbar-title>Roles</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>

            <v-text-field
              class="text-xs-center"
              v-model="search"
              append-icon="search"
              label="Búsqueda"
              single-line
              hide-details
            ></v-text-field>
            <v-spacer></v-spacer>
          </v-toolbar>
        </template>
        
        <template v-slot:item.condicion="{ item }">
          <div v-if="item.condicion">
            <span class="blue--text">Activo</span>
          </div>
          <div v-else>
            <span class="red--text">Inactivo</span>
          </div>
        </template>

        <template v-slot:no-data>
          <v-btn color="primary" @click="listar">Resetear</v-btn>
        </template>
      </v-data-table>
      <!-- Termina Tabla CRUD -->
    </v-flex>
  </v-layout>
</template>

<script>
import axios from "axios";

export default {
  data: () => ({
    roles: [],
    dialog: false,
    headers: [
      { text: "Nombre", value: "nombre" },
      { text: "Descripción", value: "descripcion", sortable: false },
      { text: "Estado", value: "condicion", sortable: false },
    ],
    search: ""
  }),

  computed: { },

  watch: { },

  created() {
    this.listar();
  },

  methods: {
    listar() {
      let me = this;
      axios
        .get("api/Roles/Listar")
        .then(function(response) {
          me.roles = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    }

  },
};
</script>
