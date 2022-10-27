## **Examen Practico IA.**

#### **Introducción**:
Examen practico para diseñar un sistema academico de magia, se debe realizar registros de solicitudes de los jovenes con mana para convertirse en grandes magos del **Reino Trébol.**

#### **Paquetes Instalados:**

        Microsoft.AspNetCore.Identity.EntityFrameworkCore,
        Microsoft.AspNetCore.JsonPatch,
        Microsoft.AspNetCore.Mvc,
        Microsoft.EntityFrameworkCore,
        Microsoft.EntityFrameworkCore.SqlServer,
        Microsoft.EntityFrameworkCore.Tools,
        AutoMapper,
        AutoMapper.Extensions.Microsoft.DependencyInjection
		
#### **Instalación:**
Una vez clonado el proyecto del repositorio, debes asegurar que tengas instalado los siguientes paquetes para poder utilizar **SqlServer** en la aplicacion y tambien para usar los comandos que nos proporciona** Microsoft.EntityFrameworkCore.Tools**:

***- Microsoft.EntityFrameworkCore.SqlServer*
*- Microsoft.EntityFrameworkCore.Tools***

Abrimos la** Consola del Administrador de paquetes** desde => **Herramientas** => **Administrador de paquetes NuGet** => **Consola del Administrador de paquetes.**
Para crear la migración de la base de datos y actualizarla en nuestro localdb. (asegurarse de que este seleccionado el proyecto** ExamIA.BL** para realizar la migracion de la base de datos.)

Posteriormente ejecutar los comandos:
            - Add-Migration Initial (preparar las tablas para la migración)
            - Update-Database (crear la base de datos con la migración)

Finalizando este ultimo paso nos aseguramos que la base de datos fue creada correctamente en nuestra** base de datos local** (**mssqllocaldb**), para eso nos vamos a =>**Ver** => **Explorador de objetos de SQL Server** y debe tener por nombre **ExamIA**.

Con esto ya podemos ejecutar el proyecto.

##### **Notas**:
La asignacion del estatus de la solicitudes son las siguientes :

        Pendiente = 1,
        Aprobado = 2,
        Rechazado = 3

esto cuando se vaya a actualizar el **Estatus** de la **Solicitud** agregar el valor de la clase enum EstatusSolicitudes.

Para poder actualizar el **Estatus** de la **Solicitud** se debe usar el **HTTP Patch** con los datos del body como en el siguiente ejemplo:

    [
       {
           "op": "replace",
           "path": "/Estatus",
           "value": 3
       }
	]

Donde **"op"** es el tipo de operacion, en este caso se usa el **replace** para el uso del **update**.
el **"Path"** es para indicar que **columna** es la que se va a **actualizar** y finalmente** "value"** es el valor de la actualizacion en este caso es el 3 (**Rechazado**).
