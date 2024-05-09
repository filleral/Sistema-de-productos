# Sistema de Gestión de Productos

Este es un sistema de gestión de productos que proporciona una API REST para administrar productos y usuarios. Utiliza Swagger para ofrecer una visión general y panorámica de todos los endpoints creados.

Recordar abrir este archivo con visual studio

![image](https://github.com/filleral/Sistema-de-productos/assets/65782602/9cebeaf0-360c-4ecc-8f2b-5a53eb3be299)


## Configuración

- Abra la solución en Visual Studio (se recomienda).
- La base de datos está en línea, por lo que no es necesario preocuparse por crear una base de datos local.

## Instalación de Dependencias

Asegúrese de instalar todas las dependencias necesarias para el correcto funcionamiento de la aplicación. Se recomienda utilizar .NET 8.0, que puede ser verificado en el código fuente.

## Funcionalidades Destacadas

- **Autenticación y Registro:** El proyecto cuenta con lógica de inicio de sesión y autenticación totalmente funcional. Se requiere autenticación para ciertos endpoints.
- **Seguridad:** Se utiliza HTTPS para garantizar la seguridad de las comunicaciones.

## Endpoints Más Utilizados

Aquí están los endpoints más utilizados que se pueden probar utilizando Postman. Algunos de estos endpoints requieren autorización, por lo que es necesario crear un usuario antes de probarlos:

- `POST` https://localhost:7046/Producto/create  
  Crea un nuevo producto.

- `GET` https://localhost:7046/Producto/getall  
  Obtiene todos los productos.

- `POST` https://localhost:7046/Producto/edit  
  Edita un producto existente.

- `GET` https://localhost:7046/Producto/getbyname  
  Obtiene un producto por nombre.

- `GET` https://localhost:7046/Producto/getbyprice  
  Obtiene productos por rango de precio.

- `POST` https://localhost:7046/Producto/delete  
  Elimina un producto existente.

- `POST` https://localhost:7046/User/create  
  Crea un nuevo usuario.

- `POST` https://localhost:7046/User/Createrol?Rolid=1&userid=1
  Crea un nuevo rol. (tener en cuentas id)

- `POST` https://localhost:7046/api/Auth/login  
  Inicia sesión y obtiene un token de acceso.

- `GET` https://localhost:7046/api/Auth/token  
  Obtiene los datos de un token de acceso.

## Notas Importantes

- Antes de probar los endpoints que requieren autorización, asegúrese de haber iniciado sesión y obtenido un token de acceso.
- La documentación completa de la API se puede encontrar utilizando Swagger en la ruta principal de la aplicación.
