# Backend - Comité Olímpico Guatemalteco

Este proyecto corresponde al backend desarrollado para gestionar solicitudes relacionadas con el **Comité Olímpico Guatemalteco**. Proporciona una API RESTful construida con **ASP.NET Core**, facilitando la manipulación de datos mediante operaciones CRUD.

---

## Características principales

- **Gestín de solicitudes**: Crear, leer, actualizar y eliminar solicitudes.
- **Arquitectura en capas**: Diseño modular para separar responsabilidades entre controladores, modelos y servicios.
- **Base de datos**: Integración con bases de datos relacionales utilizando **Entity Framework Core**.
- **Validaciones y manejo de errores**: Implementa controles de entrada y logs para un correcto funcionamiento.

---

## Tecnologías utilizadas

- **Lenguaje**: C#
- **Framework**: ASP.NET Core
- **Base de datos**: Entity Framework Core
- **Herramientas adicionales**: 
  - 	Manejo de configuración con `appsettings.json`.
  - 	Logger integrado para monitorear errores y actividades.

---

## Rutas del API

| Método | Ruta                                | Descripción                                      |
|--------|-------------------------------------|--------------------------------------------------|
| `GET`  | `/gestiones/viewAll`                | Recupera todas las solicitudes registradas.      |
| `POST` | `/gestiones/newSolicitud`           | Crea una nueva solicitud.                        |
| `PUT`  | `/gestiones/updateGestion/{id}`     | Actualiza el tipo de solicitud basado en su ID.  |
| `DELETE`| `/gestion/delete/{id}`             | Elimina una solicitud según su ID.               |
| `GET`  | `/gestiones/{palabraClave}`         | Busca solicitudes por palabra clave.             |

---
