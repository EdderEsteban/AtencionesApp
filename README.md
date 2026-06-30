# 🏥 Atenciones de Salud — San Luis

> Sistema web + API REST para digitalizar las **planillas de atención diaria** de enfermería y odontología de los hospitales y centros de salud de la provincia de San Luis, Argentina. Reemplaza los libros de Excel que se consolidaban a mano por una plataforma centralizada, con reportes y una API lista para una app móvil.

<p align="left">
  <img alt=".NET" src="https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white">
  <img alt="C#" src="https://img.shields.io/badge/C%23-MVC%20%2B%20Web%20API-239120?logo=csharp&logoColor=white">
  <img alt="EF Core" src="https://img.shields.io/badge/EF%20Core-8.0-512BD4">
  <img alt="MySQL" src="https://img.shields.io/badge/MySQL-XAMPP-4479A1?logo=mysql&logoColor=white">
  <img alt="Bootstrap" src="https://img.shields.io/badge/Bootstrap-5-7952B3?logo=bootstrap&logoColor=white">
  <img alt="JWT" src="https://img.shields.io/badge/Auth-Cookie%20%2B%20JWT-000000?logo=jsonwebtokens&logoColor=white">
</p>

---

## 📋 Descripción

Aplicación pensada para el día a día de los efectores de salud pública:

- **Enfermería y odontología** cargan sus atenciones diarias por paciente.
- **Directores** auditan la actividad de su institución; el **administrador** gestiona todo el sistema y puede ver datos globales o por efector.
- Cada institución trabaja sobre el padrón **compartido** de pacientes (la historia clínica sigue al paciente, no a la institución).
- Genera **reportes con gráficos** y **exportación a Excel**, y expone una **API REST con JWT** para una futura app Android.

---

## ✨ Características

### Aplicación web (MVC)
- 🔐 **Autenticación por cookie** con 4 roles y permisos diferenciados.
- 👥 **Pacientes**: alta/edición, búsqueda por DNI/nombre/apellido y **ficha clínica** con timeline cronológico (enfermería + odontología combinados).
- 🩺 **Atenciones de enfermería**: carga de prestaciones por grupo, tipo de atención, embarazo, cobertura.
- 🦷 **Atenciones de odontología**: prestaciones, diagnóstico CIE-10, tipo de turno y un **odontograma interactivo** (52 piezas FDI, 5 superficies por diente, 6 estados) con **índice CPO/ceo calculado automáticamente**.
- 📊 **Dashboards por rol** con Chart.js (actividad semanal, distribución, tendencia mensual, top 10 prestaciones).
- 📈 **Módulo de reportes** (enfermería / odontología) con filtros por período y profesional, y **exportación a Excel** (ClosedXML).
- ⚙️ **Administración**: gestión de usuarios (asignación multi-institución) y catálogos de prestaciones.
- 🌗 **Tema claro/oscuro**, layout responsive con sidebar + navbar.

### API REST (para app móvil)
- 🔑 **JWT Bearer** como segundo esquema de autenticación (la web sigue con cookies).
- 🏥 La **institución activa viaja dentro del token**; selección post-login para usuarios multi-institución.
- 📱 Endpoints para todo el flujo de campo: búsqueda y ficha de paciente, carga y detalle de atenciones, datos del dashboard.
- 🧮 El **CPO odontológico se calcula en el servidor** (única fuente de verdad: la app solo envía los estados de las piezas).

---

## 🛠️ Stack tecnológico

| Capa | Tecnología |
|------|-----------|
| Backend | ASP.NET Core 8 (MVC + Web API) |
| ORM | Entity Framework Core 8 + Pomelo MySQL 8.0.2 |
| Base de datos | MySQL (XAMPP) |
| Frontend | Razor + Bootstrap 5 + Chart.js |
| Auth | Cookie Authentication + JWT Bearer |
| Excel | ClosedXML 0.105 |
| Seguridad | Rate limiting nativo .NET 8 + lockout de login |

---

## 👤 Roles y permisos

| Rol | Crear | Leer | Editar | Eliminar |
|-----|:-----:|:----:|:------:|:--------:|
| **Administrador** | ✓ | ✓ | ✓ | ✓ (borrado lógico) |
| **Director** | ✗ | ✓ | ✗ | ✗ |
| **Enfermero** | ✓ | ✓ | ✓ (solo propias, mismo día) | ✗ |
| **Odontólogo** | ✓ | ✓ | ✓ (solo propias, mismo día) | ✗ |

---

## 🗄️ Modelo de datos

14 tablas principales con **borrado lógico** (Global Query Filter de EF Core), entre ellas:

`Usuarios` · `Roles` · `Instituciones` (532 efectores reales de San Luis, seed desde SISA) · `Pacientes` · `UsuarioInstituciones` (M:N) · `AtencionesPacienteEnfermeria` / `Odontologia` · `PrestacionesEnfermeria` / `Odontologia` · `TiposPrestacion*` · `Diagnosticos` (CIE-10) · `ValoracionDental` (CPO) · `OdontogramaEstado`.

> La **edad** se guarda como snapshot histórico al momento de la atención. La cobertura se registra solo como **con/sin obra social** (dato sanitario relevante, sin almacenar el nombre).

---

## 🔌 API REST — endpoints principales

| Método | Ruta | Descripción |
|--------|------|-------------|
| `POST` | `/api/auth/login` | Login → token JWT + instituciones del usuario |
| `POST` | `/api/auth/institucion` | Selecciona institución activa → token nuevo |
| `GET`  | `/api/pacientes?q=` | Búsqueda de pacientes (DNI / nombre / apellido) |
| `GET`  | `/api/pacientes/{id}` | Ficha + historia clínica completa |
| `GET`  | `/api/atenciones-enfermeria/tipos-prestacion` | Catálogo para el formulario |
| `POST` | `/api/atenciones-enfermeria` | Carga de atención de enfermería |
| `GET`  | `/api/atenciones-enfermeria/{id}` | Detalle de atención |
| `GET`  | `/api/atenciones-odontologia/tipos-prestacion` · `/diagnosticos` | Catálogos |
| `POST` | `/api/atenciones-odontologia` | Carga (con odontograma; CPO calculado en server) |
| `GET`  | `/api/atenciones-odontologia/{id}` | Detalle de atención |
| `GET`  | `/api/dashboard` | Series para las 4 gráficas del profesional |

Todas las rutas (salvo el login) requieren header `Authorization: Bearer <token>`.

---

## 🔒 Seguridad

- Contraseñas con `PasswordHasher` (nunca expuestas; la API devuelve **DTOs**, no entidades).
- **Sin mass assignment**: `UsuarioId`, `InstitucionId`, `Fecha` y `Edad` se fijan en el servidor.
- **Scoping por institución** en carga, listados, reportes y dashboard (la historia clínica, en cambio, es compartida por continuidad de atención).
- **Rate limiting** (.NET 8) global + políticas para login y reportes, con partición por usuario para tolerar NAT.
- **Lockout** de login por email (anti fuerza bruta) y `[ValidateAntiForgeryToken]` en formularios.
- Edición restringida a **autoría + ventana del mismo día** para profesionales.

---

## 🚀 Cómo ejecutar (entorno local)

**Requisitos:** [.NET 8 SDK](https://dotnet.microsoft.com/download) · MySQL (XAMPP) · IDE (Visual Studio / Rider / VS Code).

```bash
# 1. Levantar MySQL (XAMPP)
# 2. Configurar la cadena de conexión en appsettings.json (ConnectionStrings:DefaultConnection)
# 3. Aplicar migraciones (crea la BD y carga los seeds)
dotnet ef database update

# 4. Ejecutar
dotnet run
```

La app queda disponible en `http://localhost:5237`.

### 🔑 Datos de prueba (seed)
| Rol | Email | Contraseña |
|-----|-------|-----------|
| Administrador | `admin@salud.com` | `Admin123!` |

> El seed crea roles, ~91 prestaciones, 532 instituciones (SISA), 30 diagnósticos CIE-10 y el usuario administrador. Los demás usuarios se crean desde **Administración**.

---

## 📱 Roadmap

- [ ] **App Android (Java)** que consume esta API REST: búsqueda de paciente, carga de atenciones, ficha, detalle y dashboard con [MPAndroidChart](https://github.com/PhilJay/MPAndroidChart).
- [ ] Vincular prestaciones odontológicas a piezas dentales específicas (tabla `PrestacionDiente`).

---

## 📸 Capturas

> _Próximamente — agregar imágenes de la ficha de paciente, el odontograma y los dashboards._

<!-- Ejemplo: ![Dashboard](docs/dashboard.png) -->

---

## 📝 Licencia y contexto

Proyecto académico (tesis de grado). Los datos de instituciones provienen del registro **SISA** público de San Luis.
