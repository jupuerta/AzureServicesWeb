# AzureServicesWeb
En este proyecto he realizado una página web que recoge los datos de varios contenedores de **Azure Storage**, donde me he conectado a ellos mediante **tokens SAS**.

Para los servicios de **Azure Cognitive** he creado uno general (así me evito crear un servicio cognitivo cada vez, además de evitarme generar varias Keys y Endpoints para cada uno de ellos)
Consta de 3 servicios principales:
* Identificar el sentimiento que posee un texto (positivo, negativo o neutro)
* Extraer el texto que hay en una imagen
* Detectar los diferentes objetos que se encuentran en una imagen

Para procesar las peticiones y para que en última instancia el controller solo tenga que enviar los datos a la vista he realizado una parte "Servicios". En ella, he creado una interfaz padre de la que heredará una clase abstracta en donde implementaré la parte común a todos los servicios hijos, relativo a cada controller.

En el ejercicio de detectar los objetos en una imagen muchas veces el resultado era algo pobre, por lo que he utilizado el servicio del **Análisis de Imagen** que muestra las palabras clave que representa una imagen.

Al mostrar las líneas en las tablas de resultados he utilizado **bootstrap** para que cambie un poco el color de las líneas alternas a modo de cebra. 

A la hora de mostrar los resultados ha habido alguna limitación:
* En TextoEnFichero, la API de Azure tenía una restricción de procesar a la vez solo 10 ficheros.
* En TextoEnImagen y ObjetoEnImagen, el tiempo de respuesta era muy elevado por lo que se han reducido el número de fotos a procesar.

