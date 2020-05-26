<template>
  <v-layout align-start>
    <v-flex>
      <!-- Tabla CRUD -->
      <v-data-table
        :headers="headers"
        :items="usuarios"
        :search="search"
        sort-by="nombre"
        class="elevation-1"
      >
        <template v-slot:top>
          <v-toolbar flat color="white">
            <v-toolbar-title>Usuarios</v-toolbar-title>
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
                      <v-col cols="6" sm="6" md="6">
                        <v-text-field
                          v-model="nombre"
                          label="Nombre"
                        ></v-text-field>
                      </v-col>

                      <v-col cols="6" sm="6" md="6">
                        <v-select v-model="idrol" :items="roles" label="Rol">
                        </v-select>
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

                      <v-col cols="6" sm="6" md="6">
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

                      <v-col cols="6" sm="6" md="6">
                        <v-text-field
                          v-model="password"
                          type="password"
                          label="Contraseña"
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

            <!-- Modal para activar y desactivar el usuario -->
            <v-dialog v-model="adModal" max-width="390px">
              <v-card>
                <v-card-title class="headline" v-if="adAccion == 1"
                  >¿Activar usuario?</v-card-title
                >
                <v-card-title class="headline" v-if="adAccion == 2"
                  >¿Desactivar usuario?</v-card-title
                >

                <v-card-text>
                  Estás a punto de
                  <span v-if="adAccion == 1">activar</span>
                  <span v-if="adAccion == 2">desactivar</span>
                  el siguiente usuario: {{ adNombre }}
                </v-card-text>
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn
                    color="green darken-1"
                    text
                    small
                    @click="activarDesactivarCerrar"
                    >Cancelar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 1"
                    color="orange darken-4"
                    text
                    small
                    @click="activar"
                    >Activar</v-btn
                  >
                  <v-btn
                    v-if="adAccion == 2"
                    color="orange darken-4"
                    text
                    small
                    @click="desactivar"
                    >Desactivar</v-btn
                  >
                </v-card-actions>
              </v-card>
            </v-dialog>

          </v-toolbar>
        </template>

        <!-- Muestra Activo/Inactivo -->
        <template v-slot:item.condicion="{ item }">
          <div v-if="item.condicion">
            <span class="blue--text">Activo</span>
          </div>
          <div v-else>
            <span class="red--text">Inactivo</span>
          </div>
        </template>

        <!-- Activar y desactivar Iconos -->
        <template v-slot:item.actions="{ item }">
          <td class="justify-center layout px-0">
            <v-icon small class="mr-2" @click="editItem(item)">
              edit
            </v-icon>

            <template v-if="item.condicion">
              <v-icon small @click="activarDesactivarMostrar(2, item)">
                block
              </v-icon>
            </template>
            <template v-else>
              <v-icon small @click="activarDesactivarMostrar(1, item)">
                check
              </v-icon>
            </template>
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
    usuarios: [],
    dialog: false,
    headers: [
      { text: "Opciones", value: "actions", sortable: false },
      { text: "Nombre", value: "nombre" },
      { text: "Rol", value: "rol" },
      { text: "Tipo documento", value: "tipo_documento" },
      { text: "Número documento", value: "num_documento", sortable: false },
      { text: "Dirección", value: "direccion", sortable: false },
      { text: "Teléfono", value: "telefono", sortable: false },
      { text: "Correo eléctronico", value: "email", sortable: false },
      { text: "Estado", value: "condicion", sortable: false },
    ],
    search: "",
    editedIndex: -1,
    id: "",
    idrol: "",
    roles: [],
    nombre: "",
    tipo_documento: "",
    documentos: ["INE", "PASAPORTE", "LICENCIA MANEJO", "INAPAM", "CEDULA"],
    num_documento: "",
    direccion: "",
    telefono: "",
    email: "",
    password: "",
    act_password: false,
    passwordAnt: "",
    valida: 0,
    validaMensaje: [],
    adModal: 0,
    adAccion: 0,
    adNombre: "",
    adId: "",
  }),

  computed: {
    formTitle() {
      return this.editedIndex === -1 ? "Nuevo usuario" : "Actualizar usuario";
    },
  },

  watch: {
    dialog(val) {
      val || this.close();
    },
  },

  created() {
    this.listar();
    this.select();
  },

  methods: {
    listar() {
      let me = this;
      axios
        .get("api/Usuarios/Listar")
        .then(function(response) {
          me.usuarios = response.data;
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    select() {
      let me = this;
      var rolesArray = [];
      axios
        .get("api/Roles/Select")
        .then(function(response) {
          rolesArray = response.data;
          rolesArray.map(function(x) {
            me.roles.push({ text: x.nombre, value: x.idrol });
          });
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    editItem(item) {
      this.id = item.idusuario;
      this.idrol = item.idrol;
      this.nombre = item.nombre;
      this.tipo_documento = item.tipo_documento;
      this.num_documento = item.num_documento;
      this.direccion = item.direccion;
      this.telefono = item.telefono;
      this.email = item.email;
      this.password = item.password_hash;
      this.passwordAnt = item.password_hash;
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

        if (me.password != me.passwordAnt) {
          me.act_password = true;
        }

        axios
          .put("api/Usuarios/Actualizar", {
            idusuario: me.id,
            idrol: me.idrol,
            nombre: me.nombre,
            tipo_documento: me.tipo_documento,
            num_documento: me.num_documento,
            direccion: me.direccion,
            telefono: me.telefono,
            email: me.email,
            password: me.password,
            act_password: me.act_password,
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
          .post("api/Usuarios/Crear", {
            idusuario: me.idusuario,
            idrol: me.idrol,
            nombre: me.nombre,
            tipo_documento: me.tipo_documento,
            num_documento: me.num_documento,
            direccion: me.direccion,
            telefono: me.telefono,
            email: me.email,
            password: me.password,
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
      this.idrol = "";
      this.nombre = "";
      this.tipo_documento = "";
      this.num_documento = "";
      this.direccion = "";
      this.telefono = "";
      this.email = "";
      this.password = "";
      this.act_password = false;
      this.passwordAnt = "";
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

      if (!this.idrol) {
        this.validaMensaje.push("Seleccione un rol.");
      }

      if (!this.tipo_documento) {
        this.validaMensaje.push("Seleccione un documento.");
      }

      if (!this.email) {
        this.validaMensaje.push("Ingrese un correo electrónico válido.");
      }

      if (!this.password) {
        this.validaMensaje.push("Ingrese la contraseña del usuario.");
      }

      if (this.validaMensaje.length) {
        this.valida = 1;
      }
      return this.valida;
    },

    activarDesactivarMostrar(accion, item) {
      this.adModal = 1;
      this.adNombre = item.nombre;
      this.adId = item.idusuario;

      if (accion == 1) {
        this.adAccion = 1;
      } else if (accion == 2) {
        this.adAccion = 2;
      } else {
        this.adAccion = 0;
      }
    },

    activarDesactivarCerrar() {
      this.adModal = 0;
    },

    activar() {
      // Código para Guardar una edición
      let me = this;
      axios
        .put("api/Usuarios/Activar/" + this.adId, {})
        .then(function(response) {
          me.adModal = 0;
          me.adAccion = 0;
          me.adNombre = "";
          me.adId = "";
          me.listar();
        })
        .catch(function(error) {
          console.log(error);
        });
    },

    desactivar() {
      let me = this;
      axios
        .put("api/Usuarios/Desactivar/" + this.adId, {})
        .then(function(response) {
          me.adModal = 0;
          me.adAccion = 0;
          me.adNombre = "";
          me.adId = "";
          me.listar();
        })
        .catch(function(error) {
          console.log(error);
        });
    },
  },
};
</script>
