# 🏛️ DGII - Sistema de Gestión de Contribuyentes

> API REST para la gestión de contribuyentes y comprobantes fiscales de la Dirección General de Impuestos Internos (DGII) de República Dominicana.

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=flat)](https://docs.microsoft.com/en-us/ef/core/)
[![SQLite](https://img.shields.io/badge/SQLite-3-003B57?style=flat&logo=sqlite)](https://www.sqlite.org/)
[![Angular](https://img.shields.io/badge/Angular-17-DD0031?style=flat&logo=angular)](https://angular.io/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

---

## 📋 Tabla de Contenidos

- [Descripción](#-descripción)
- [Características](#-características)
- [Tecnologías](#-tecnologías)
- [Arquitectura](#-arquitectura)
- [Estructura del Proyecto](#-estructura-del-proyecto)
- [Requisitos Previos](#-requisitos-previos)
- [Instalación](#-instalación)
- [Configuración](#-configuración)
- [Ejecución](#-ejecución)
- [Endpoints de la API](#-endpoints-de-la-api)
- [Principios SOLID Aplicados](#-principios-solid-aplicados)
- [Autor](#-autor)

---

## 📖 Descripción

Sistema desarrollado como prueba técnica para la DGII que permite:

- ✅ Gestionar contribuyentes (personas físicas y jurídicas)
- ✅ Administrar comprobantes fiscales (NCF)
- ✅ Calcular automáticamente el ITBIS (Impuesto sobre Transferencias de Bienes Industrializados y Servicios)
- ✅ Generar reportes con totales de ITBIS por contribuyente
- ✅ Consumir la API desde una aplicación web Angular

---

## ✨ Características

### Backend (API REST)

- 🔐 **Arquitectura en Capas**: Domain, Application, Infrastructure, API
- 🎯 **Principios SOLID**: Código mantenible y escalable
- 📝 **Logging**: Registro de operaciones y errores en todas las capas
- ⚠️ **Manejo de Excepciones**: Excepciones personalizadas por capa
- 💉 **Dependency Injection**: Inyección de dependencias nativa de .NET
- 📚 **Swagger**: Documentación interactiva de la API
- 🗄️ **Entity Framework Core**: ORM para acceso a datos
- 🔄 **Repository Pattern**: Abstracción del acceso a datos
- 🎨 **DTOs**: Separación entre entidades de dominio y transferencia de datos

### Frontend (Angular)

- 🅰️ **Angular 17**: Framework modular y reactivo
- 🎨 **Diseño Responsivo**: Interfaz adaptable a diferentes dispositivos
- 🔗 **Integración con API**: Consumo de endpoints REST
- 📊 **Visualización de Datos**: Tablas y reportes interactivos

---

## 🛠️ Tecnologías

### Backend

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| .NET Core | 8.0 | Framework principal |
| ASP.NET Core Web API | 8.0 | Creación de API REST |
| Entity Framework Core | 8.0 | ORM para base de datos |
| SQLite | 3 | Base de datos (desarrollo) |
| Swagger/OpenAPI | 6.5 | Documentación de API |
| Serilog | - | Sistema de logging |

### Frontend

| Tecnología | Versión | Propósito |
|------------|---------|-----------|
| Angular | 17+ | Framework SPA |
| TypeScript | 5.x | Lenguaje tipado |
| RxJS | 7.x | Programación reactiva |
| Bootstrap/Angular Material | - | Componentes UI |

---

## 🏗️ Arquitectura

El proyecto sigue una **arquitectura en capas (Clean Architecture)** que separa responsabilidades:

```
┌─────────────────────────────────────────────────────────┐
│                     🌐 API Layer                        │
│               (Controllers, Middleware)                 │
└────────────────────┬────────────────────────────────────┘
                     │ Depende de
                     ↓
┌─────────────────────────────────────────────────────────┐
│                 💼 Application Layer                    │
│              (Services, DTOs, Interfaces)               │
└────────────────────┬────────────────────────────────────┘
                     │ Depende de
                     ↓
┌─────────────────────────────────────────────────────────┐
│                   🎯 Domain Layer                       │
│        (Entities, Enums, Exceptions, Interfaces)        │
└────────────────────┬────────────────────────────────────┘
                     ↑ Implementa
                     │
┌─────────────────────────────────────────────────────────┐
│                🔧 Infrastructure Layer                  │
│           (Repositories, DbContext, Data Access)        │
└─────────────────────────────────────────────────────────┘
```

### Ventajas de esta Arquitectura:

✅ **Independencia de frameworks**: El dominio no depende de tecnologías específicas  
✅ **Testeable**: Fácil crear pruebas unitarias  
✅ **Mantenible**: Cambios aislados en cada capa  
✅ **Escalable**: Fácil agregar nuevas funcionalidades  

---

## 📁 Estructura del Proyecto

```
DGII-Solution/
│
├── Backend/
│   ├── DGII.API/                    # Capa de Presentación
│   │   ├── Controllers/             # REST Controllers
│   │   │   ├── TaxpayerController.cs
│   │   │   └── TaxReceiptController.cs
│   │   ├── Middleware/              # Middlewares personalizados
│   │   ├── Program.cs               # Configuración de la app
│   │   ├── appsettings.json         # Configuración
│   │   └── DGII.API.csproj
│   │
│   ├── DGII.Application/            # Capa de Aplicación
│   │   ├── DTOs/                    # Data Transfer Objects
│   │   │   ├── TaxpayerDto.cs
│   │   │   ├── TaxReceiptDto.cs
│   │   │   ├── TaxpayerReportDto.cs
│   │   │   └── TaxReceiptListDto.cs
│   │   ├── Services/                # Lógica de negocio
│   │   │   ├── TaxpayerService.cs
│   │   │   └── TaxReceiptService.cs
│   │   └── DGII.Application.csproj
│   │
│   ├── DGII.Domain/                 # Capa de Dominio
│   │   ├── Entities/                # Entidades del negocio
│   │   │   ├── Taxpayer.cs
│   │   │   └── TaxReceipt.cs
│   │   ├── Enums/                   # Enumeraciones
│   │   │   ├── TaxpayerType.cs
│   │   │   └── TaxpayerStatus.cs
│   │   ├── Interfaces/              # Contratos
│   │   │   ├── ITaxpayerRepository.cs
│   │   │   └── ITaxReceiptRepository.cs
│   │   ├── Exceptions/              # Excepciones del dominio
│   │   └── DGII.Domain.csproj
│   │
│   ├── DGII.Infrastructure/         # Capa de Infraestructura
│   │   ├── Data/                    # DbContext y configuraciones
│   │   │   └── ApplicationDbContext.cs
│   │   ├── Repositories/            # Implementación de repositorios
│   │   │   ├── TaxpayerRepository.cs
│   │   │   └── TaxReceiptRepository.cs
│   │   └── DGII.Infrastructure.csproj
│   │
│   └── DGII.sln                     # Archivo de solución
│
└── Frontend/
    └── dgii-angular-app/            # Aplicación Angular
        ├── src/
        │   ├── app/
        │   │   ├── core/            # Servicios singleton
        │   │   ├── shared/          # Componentes compartidos
        │   │   ├── features/        # Módulos por funcionalidad
        │   │   └── models/          # Interfaces TypeScript
        │   └── environments/
        └── angular.json
```

---

## 📋 Requisitos Previos

Asegúrate de tener instalado:

- ✅ [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- ✅ [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)
- ✅ [Node.js 18+](https://nodejs.org/) (para Angular)
- ✅ [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)
- ✅ [Git](https://git-scm.com/)

---

## 🚀 Instalación

### 1️⃣ Clonar el Repositorio

```bash
git clone https://github.com/KenelCruz/dgii-sistema-contribuyentes.git
cd dgii-sistema-contribuyentes
```

### 2️⃣ Backend - Restaurar Paquetes

```bash
cd Backend
dotnet restore
dotnet build
```

### 3️⃣ Frontend - Instalar Dependencias

```bash
cd Frontend/dgii-angular-app
npm install
```

---

## ⚙️ Configuración

### Backend (appsettings.json)

El archivo `appsettings.json` ya está configurado con SQLite:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=dgii.db"
  }
}
```

**Para usar SQL Server** (producción), modifica la connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=DGII_DB;Trusted_Connection=true;"
}
```

Y en `Program.cs` cambia:

```csharp
// De esto:
options.UseSqlite(...)

// A esto:
options.UseSqlServer(...)
```

### Frontend (environment.ts)

Configura la URL del API en `src/environments/environment.ts`:

```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

---

## ▶️ Ejecución

### Backend

#### Opción 1: Visual Studio
1. Abre `DGII.sln`
2. Establece `DGII.API` como proyecto de inicio
3. Presiona `F5` o click en ▶️

#### Opción 2: Terminal
```bash
cd Backend/DGII.API
dotnet run
```

La API estará disponible en:
- 🌐 **Swagger UI**: `https://localhost:7206`
- 🔗 **API Base URL**: `https://localhost:7206/api`

### Frontend

```bash
cd Frontend/dgii-angular-app
ng serve
```

La aplicación web estará disponible en:
- 🌐 `http://localhost:4200`

---

## 📡 Endpoints de la API

### 👥 Contribuyentes

#### Listar Todos los Contribuyentes
```http
GET /api/taxpayer
```

**Respuesta:**
```json
[
  {
    "rncCedula": "98754321012",
    "nombre": "JUAN PEREZ",
    "tipo": "PERSONA FISICA",
    "estatus": "activo"
  },
  {
    "rncCedula": "123456789",
    "nombre": "FARMACIA TU SALUD",
    "tipo": "PERSONA JURIDICA",
    "estatus": "inactivo"
  }
]
```

#### Obtener Reporte de Contribuyente
```http
GET /api/taxpayer/{rncCedula}
```

**Ejemplo:** `GET /api/taxpayer/98754321012`

**Respuesta:**
```json
{
  "rncCedula": "98754321012",
  "name": "JUAN PEREZ",
  "totalItbis": 216.00,
  "vouchers": [
    {
      "ncf": "E310000000001",
      "amount": 200.00,
      "itbis": 36.00
    },
    {
      "ncf": "E310000000002",
      "amount": 1000.00,
      "itbis": 180.00
    }
  ]
}
```

### 📄 Comprobantes Fiscales

#### Listar Todos los Comprobantes
```http
GET /api/taxreceipt
```

**Respuesta:**
```json
[
  {
    "rncCedula": "98754321012",
    "ncf": "E310000000001",
    "monto": "200.00",
    "itbis18": "36.00"
  },
  {
    "rncCedula": "98754321012",
    "ncf": "E310000000002",
    "monto": "1000.00",
    "itbis18": "180.00"
  }
]
```

#### Listar Comprobantes por Contribuyente
```http
GET /api/taxreceipt/taxpayer/{rncCedula}
```

**Ejemplo:** `GET /api/taxreceipt/taxpayer/98754321012`

---

## 🎯 Principios SOLID Aplicados

### **S - Single Responsibility Principle**
✅ Cada clase tiene una única responsabilidad:
- `TaxpayerService`: Solo lógica de contribuyentes
- `TaxpayerRepository`: Solo acceso a datos de contribuyentes
- `TaxpayerController`: Solo manejo de peticiones HTTP

### **O - Open/Closed Principle**
✅ Código abierto para extensión, cerrado para modificación:
- Nuevos repositorios se agregan sin modificar existentes
- Nuevas excepciones heredan de `DomainException`

### **L - Liskov Substitution Principle**
✅ Las implementaciones pueden sustituir a sus interfaces:
- `ITaxpayerRepository` puede ser `TaxpayerRepository` o cualquier otra implementación
- Fácil crear mocks para testing

### **I - Interface Segregation Principle**
✅ Interfaces específicas y enfocadas:
- `ITaxpayerRepository`: Solo métodos de contribuyentes
- `ITaxReceiptRepository`: Solo métodos de comprobantes

### **D - Dependency Inversion Principle**
✅ Dependencias en abstracciones, no implementaciones:
- Controllers dependen de `IServices`
- Services dependen de `IRepositories`
- Configurado con Dependency Injection

---

## 🧪 Testing

### Backend - Pruebas con Swagger

1. Ejecuta el proyecto
2. Abre `https://localhost:7206`
3. Prueba cada endpoint con el botón "Try it out"

### Frontend - Pruebas End-to-End

```bash
cd Frontend/dgii-angular-app
ng test
```

---

## 📝 Datos de Prueba

La aplicación incluye datos precargados:

| RNC/Cédula | Nombre | Tipo | Comprobantes | Total ITBIS |
|------------|--------|------|--------------|-------------|
| 98754321012 | JUAN PEREZ | Persona Física | 2 | $216.00 |
| 123456789 | FARMACIA TU SALUD | Persona Jurídica | 0 | $0.00 |

---

## 🐛 Troubleshooting

### Error: "Unable to resolve service"
**Solución:** Verifica que todos los servicios estén registrados en `Program.cs`:
```csharp
builder.Services.AddScoped<TaxpayerService>();
builder.Services.AddScoped<TaxReceiptService>();
```

### Error: CORS
**Solución:** Asegúrate que el backend permite el origen de Angular en `Program.cs`:
```csharp
policy.WithOrigins("http://localhost:4200")
```

### Base de datos no se crea
**Solución:** Verifica que existe la carpeta del proyecto y ejecuta:
```csharp
context.Database.EnsureCreated();
```

---

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

---

## 👨‍💻 Autor

**Kenel Cruz**

- 🐙 GitHub: [@KenelCruz](https://github.com/KenelCruz)
- 📧 Email: [kenelcruz@gmail.com]


---

## 🙏 Agradecimientos

- Dirección General de Impuestos Internos (DGII) por la oportunidad
- Comunidad de .NET y Angular por los recursos

---

## 📚 Recursos Adicionales

- [Documentación de .NET](https://docs.microsoft.com/dotnet/)
- [Documentación de Entity Framework Core](https://docs.microsoft.com/ef/core/)
- [Documentación de Angular](https://angular.io/docs)
- [Principios SOLID](https://en.wikipedia.org/wiki/SOLID)

---

<div align="center">

**⭐ Si te gustó este proyecto, dale una estrella en GitHub ⭐**

Hecho con ❤️ por [Kenel Cruz](https://github.com/KenelCruz)

</div>
