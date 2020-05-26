<template>
  <v-layout align-start>
    <v-flex>
      <!-- Tabla CRUD -->
      <v-data-table
        :headers="headers"
        :items="proveedores"
        :search="search"
        sort-by="nombre"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar flat color="white">
            <v-toolbar-title>Proveedores</v-toolbar-title>
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

            <!-- Dialogo para agregar/modificar un usuario -->
            <v-dialog v-model="dialog" max-width="500px">
              <template v-slot:activator="{ on }">
                <v-btn color="primary" dark class="mb-2" v-on="on">Nuevo</v-btn>
              </template>
              <v-card>
                <v-card-title>
                  <span class="headline">{{ formTitle }}</span>
                </v-card-title>

                <v-card-text>
                  <v-container>
                    <v-row>
                      <v-col cols="12" sm="12" md="12">
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="6" sm="6" md="6">
                        <v-select
                          v-model="tipo_documento"
                          :items="documentos"
                          label="Tipo documento"
                        >
                        </v-select>
                      </v-col>

                      <v-col cols="6" sm="6" md="6">
                        <v-text-field
                          v-model="num_documento"
                          label="Numero documento"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="12" sm="12" md="12">
                        <v-text-field
                          v-model="direccion"
                          label="Dirección"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="6" sm="6" md="6">
                        <v-text-field
                          v-model="telefono"
                          label="Teléfono"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="6" sm="6" md="6">
                        <v-text-field
                          v-model="email"
                          label="Correo electrónico"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="12" sm="12" md="12" v-show="valida">
                        <div
                          class="red--text"
                          v-for="v in validaMensaje"
                          :key="v"
                          v-text="v"
                        ></div>
                      </v-col>
                    </v-row>
                  </v-container>
                </v-card-text>

                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="close"
                    >Cancelar</v-btn
                  >
                  <v-btn color="blue darken-1" text @click="guardar"
                    >Guardar</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>

          </v-toolbar>
        </template>

        <!-- Activar y desactivar Iconos -->
        <template v-slot:item.actions="{ item }">
          <td class="justify-center layout px-0">
            <v-icon small class="mr-2" @click="editItem(item)">
              edit
            </v-icon>
          </td>
        </template>

        <!-- Reiniciar la carga de los datos -->
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
    proveedores: [],
    dialog: false,
    headers: [
      { text: "Opciones", value: "actions", sortable: false },
      { text: "Nombre", value: "nombre" },
      { text: "Tipo persona", value: "tipo_persona" },
      { text: "Tipo documento", value: "tipo_documento" },
      { text: "Número documento", value: "num_documento", sortable: false },
      { text: "Dirección", value: "direccion", sortable: false },
      { text: "Teléfono", value: "telefono", sortable: false },
      { text: "Correo eléctronico", value: "email", sortable: false }
    ],
    search: "",
    editedIndex: -1,
    id: "",
    nombre: "",
    tipo_documento: "",
    documentos: ["INE", "PASAPORTE", "LICENCIA MANEJO", "INAPAM", "CEDULA"],
    num_documento: "",
    direccion: "",
    telefono: "",
    email: "",
    
    valida: 0,
    validaMensaje: [],
    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: "",
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo proveedor" : "Actualizar proveedor";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  created() {
    this.listar();
  },

  methods: {
    listar() {
      let me = this;
      axios
        .get("api/Personas/ListarProveedores")
        .then(function(response) {
          me.proveedores = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    editItem(item) {
      this.id = item.idpersona; 
      this.nombre = item.nombre;
      this.tipo_documento = item.tipo_documento;
      this.num_documento = item.num_documento;
      this.direccion = item.direccion;
      this.telefono = item.telefono;
      this.email = item.email;
      this.editedIndex = 1;
      this.dialog = true;
    },

    close() {
      this.dialog = false;
      this.limpiar();
    },

    guardar() {
      if (this.validar()) {
        return;
      }

      if (this.editedIndex > -1) {
        // Código para Guardar una edición
        let me = this;

        axios
          .put("api/Personas/Actualizar", {
            'idpersona': me.id,
            'tipo_persona': 'Proveedor',
            'nombre': me.nombre,
            'tipo_documento': me.tipo_documento,
            'num_documento': me.num_documento,
            'direccion': me.direccion,
            'telefono': me.telefono,
            'email': me.email
          })
          .then(function(response) {
            me.close();
            me.listar();
            me.limpiar();
          })
          .catch(function(error) {
            console.log(error);
          });
      } else {
        // Código para Guardar
        let me = this;

        axios
          .post("api/Personas/Crear", {
            'tipo_persona': 'Proveedor',
            'nombre': me.nombre,
            'tipo_documento': me.tipo_documento,
            'num_documento': me.num_documento,
            'direccion': me.direccion,
            'telefono': me.telefono,
            'email': me.email
          })
          .then(function(response) {
            me.close();
            me.listar();
            me.limpiar();
          })
          .catch(function(error) {
            console.log(error);
          });
      }
    },

    limpiar() {
      this.id = "";
      this.nombre = "";
      this.tipo_documento = "";
      this.num_documento = "";
      this.direccion = "";
      this.telefono = "";
      this.email = "";
      this.validaMensaje = [];
      this.editedIndex = -1;
    },

    validar() {
      this.valida = 0;
      this.validaMensaje = [];

      if (this.nombre.length < 3 || this.nombre.length > 100) {
        this.validaMensaje.push(
          "El nombre debe de tener mas de 3 caracteres y menos de 100 caracteres."
        );
      }

      if (!this.tipo_documento) {
        this.validaMensaje.push("Seleccione un documento.");
      }

      if (this.validaMensaje.length) {
        this.valida = 1;
      }
      return this.valida;
    },

  },
};
</script>
