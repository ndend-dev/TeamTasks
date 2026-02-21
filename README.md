# Guía de Instalación: Scripts de Base de Datos

Este repositorio contiene los recursos necesarios para desplegar la estructura, los datos iniciales y la capa de lógica de negocio del sistema **TeamTasks**.

## Preparación Inicial

1. Abra **SQL Server Management Studio (SSMS)**.
2. Conéctese a su instancia de servidor.
3. Localice en su explorador de archivos la carpeta: **`Database`**.

---

## Pasos para la Instalación Manual

Es fundamental seguir este orden estrictamente para garantizar la integridad referencial y que los objetos se creen sin errores de dependencia.

### 1. Base de Datos y Estructura

- **Archivo:** `DbSetup_TeamTasks.sql`
- **Descripción:** Script principal que crea la base de datos, define los esquemas, las tablas y carga los datos maestros por defecto.
- **Nota:** Debe ejecutarse con éxito antes de proceder con los procedimientos almacenados.

### 2. Capa de Lógica de Negocio (Stored Procedures)

Una vez creada la base de datos, ejecute los siguientes scripts en el orden deseado para habilitar las funcionalidades de la aplicación:

| Orden | Archivo                                       | Funcionalidad                                           |
| ----- | --------------------------------------------- | ------------------------------------------------------- |
| **2** | `Core.sp_developer_delay_risk_prediction.sql` | Predicción de riesgo de retraso por desarrollador.      |
| **3** | `Core.sp_get_developer_workload.sql`          | Consulta de carga de trabajo actual por desarrollador.  |
| **4** | `Core.sp_get_project_status_summary.sql`      | Resumen ejecutivo del estado de los proyectos.          |
| **5** | `Core.sp_get_upcoming_deadlines.sql`          | Listado de tareas próximas a vencer.                    |
| **6** | `Core.sp_insert_task.sql`                     | Lógica para la inserción y validación de nuevas tareas. |

---

# Backend: Arquitectura en Capas (ASP Core 8)

El backend está diseñado siguiendo un patrón de **Arquitectura en Capas**, lo que facilita el mantenimiento, la escalabilidad y las pruebas unitarias.

## Estructura del Proyecto

### 1. Capa de API (Presentación)

Es el punto de entrada de la aplicación.

- **Contenido:** Controllers, Middlewares y configuración de Program.cs.
- **Responsabilidad:** Manejar las peticiones HTTP y devolver las respuestas al cliente.

### 2. Capa BL (Business Logic)

Aquí reside el "corazón" de la aplicación.

- **Contenido:** Servicios y lógica de validación.
- **Responsabilidad:** Procesar los datos antes de guardarlos o después de consultarlos. Es la encargada de llamar a la capa DAL.

### 3. Capa DAL (Data Access Layer)

Comunicación directa con SQL Server.

- **Contenido:** Contexto de Base de Datos (Entity Framework), Repositorios y llamadas a **Stored Procedures**.
- **Responsabilidad:** Ejecutar las consultas y comandos en la base de datos.

### 4. Capa Utils (Utilidades)

- **Contenido:** Dtos que se usan en todo el proyecto.

### 5. Capa Test

- **Contenido:** Pruebas unitarias y de integración.
- **Responsabilidad:** Asegurar que cada capa funcione correctamente de forma aislada.

## Configuración de Credenciales

Para conectar el backend con la base de datos configurada previamente:

1. Abra el archivo `appsettings.json` en la capa **API**.
2. Actualice la cadena de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=MyServer;Database=MyDatabase;User ID=User;Password=MyPassword;TrustServerCertificate=True"
  }
}
```

## Ejecución

1. Abra la solución (`.sln`) en Visual Studio 2022.
2. Establezca el proyecto **API** como proyecto de inicio.
3. Ejecute mediante el perfil de **http/https**.
4. **Swagger** se iniciará automáticamente para probar los endpoints.

<img width="518" height="941" alt="Captura de pantalla 2026-02-21 124927" src="https://github.com/user-attachments/assets/78494d8c-4db7-4f57-bb32-71e95ca40423" />


---

# Frontend: TeamTasksDashboard (Angular 21)

Esta carpeta, ubicada en **`Frontend`**, contiene la interfaz de usuario desarrollada en **Angular 21**. El diseño es completamente responsivo y utiliza herramientas modernas.

## Tecnologías Implementadas

El proyecto ya viene configurado con las siguientes dependencias (incluidas en el `package.json`):

- **Tailwind CSS**: Utilizado para todo el estilizado mediante clases de utilidad.
- **Lucide Angular**: Implementado para la iconografía de la aplicación.
- **Angular CDK**: Utilizado para la gestión de modales y componentes de interacción avanzada.
- **NGX-Toastr**: Configurado para mostrar notificaciones emergentes tras interactuar con la API.

---

## Ejecución del Proyecto

Siga estos pasos para levantar el entorno de desarrollo:

### 1. Instalación de dependencias

Navegue a la carpeta del frontend y descargue los paquetes necesarios:

```bash
cd Frontend
npm install
```

### 2. Inicio del servidor

Una vez completada la instalación, ejecute el servidor local:

```bash
ng serve
```

La aplicación estará disponible en `http://localhost:4200`.

---

## Organización del Código (`src/app`)

La arquitectura del frontend en la carpeta **`/Frontend`** se divide en cuatro pilares fundamentales para mantener el orden y la escalabilidad:

- **`pages/`**: Contiene los componentes que representan las vistas completas de la aplicación (ej: Dashboard, Projects, Project Task, Developers, etc). Actúan como contenedores de alto nivel.
- **`components/`**: Elementos de interfaz reutilizables (Tabla dinamica, Sidebar, Detalles de tareas, y creacion de tares).
- **`services/`**: Clases encargadas de la comunicación HTTP. Son las que conectan el frontend con el **Backend** para ejecutar la lógica de los Stored Procedures.
- **`interfaces/`**: Contratos de datos en TypeScript. Definen la estructura exacta de los objetos para asegurar que coincidan con los **DTOs** enviados por la API.

## Detalle de Pantallas (Pages)

Cada sección del frontend ha sido diseñada para visualizar y gestionar la información procesada por los Stored Procedures:

### Dashboard General

Es el panel principal que consolida las métricas clave de la aplicación.

- **Contenido:** Tablas dinámicas que muestran los resultados directos de los SPs de análisis y riesgos.

<img width="1900" height="949" alt="Captura de pantalla 2026-02-21 135752" src="https://github.com/user-attachments/assets/43672443-9151-48b3-a16a-147437399178" />


### Proyectos (Projects)

Listado maestro de los proyectos activos en el sistema.

- **Funcionalidad:** Muestra la información general de cada proyecto. Al hacer clic en el botón de acción de la tabla, redirige o despliega el detalle de tareas asociadas a ese proyecto específico.

<img width="1919" height="951" alt="Captura de pantalla 2026-02-21 135859" src="https://github.com/user-attachments/assets/9733e0fe-2cee-494b-a10f-4e41f445ea18" />



### Tareas por Proyecto

Vista detallada que se activa desde la pantalla de Proyectos.

- **Funcionalidad:** Filtra y visualiza únicamente las tareas pertenecientes al proyecto seleccionado.

<img width="1916" height="951" alt="Captura de pantalla 2026-02-21 135927" src="https://github.com/user-attachments/assets/3e9f342b-e50a-41ff-b525-fdfc706c8a27" />


### Registro de Tareas (Modal)

Componente emergente desarrollado con **Angular CDK**.

- **Funcionalidad:** Formulario para la creación de nuevas tareas. Utiliza el SP `Core.sp_insert_task` y confirma el éxito del registro mediante un aviso de **NGX-Toastr**.
- **Captura:**

<img width="1913" height="941" alt="Captura de pantalla 2026-02-21 135915" src="https://github.com/user-attachments/assets/9946fb55-cf31-4f3d-a813-a5e642075780" />


### Desarrolladores (Developers)

Presenta el listado completo de los desarrolladores registrados en el sistema

<img width="1918" height="953" alt="Captura de pantalla 2026-02-21 135940" src="https://github.com/user-attachments/assets/3435d6c7-8050-4d8c-a335-8a15f3ee40b1" />


### Estados de Projecto (Project Status)

Presenta el listado de los diferentes estados disponibles para las tareas.

<img width="1919" height="949" alt="Captura de pantalla 2026-02-21 140022" src="https://github.com/user-attachments/assets/fc2fde82-1d5c-4297-ba5c-94b491be1a34" />


### Estados de Tareas (Task Status)

Ppresenta el listado de los diferentes estados disponibles para las tareas.

<img width="1917" height="948" alt="Captura de pantalla 2026-02-21 135949" src="https://github.com/user-attachments/assets/6fc1f8da-3c35-4bca-bb4c-23f54b943984" />


---
