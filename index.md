# SandSimulatorC# - README

## Arquitectura

Este juego es un simulador de partículas hecho con C# y **MonoGame**, un framework de C# que maneja los gráficos del juego. El proyecto tiene una arquitectura orientada en componentes, separando lógica, gráficos, interfaz y multijugador.

El core de la aplicación está en la clase `Game1`, que gestiona el ciclo de vida del juego y controla las interacciones entre los siguientes componentes:

* **GridManager**: Responsable de la lógica de simulación. Mantiene el estado de cada celda en la cuadrícula y actualiza los elementos según sus propiedades físicas.
* **GraphicManager**: Se encarga de renderizar la cuadrícula y los elementos en la pantalla.
* **ControllerManager**: Gestiona los inputs del usuario, permitiendo interactuar con la simulación.
* **MultiplayerClient / MultiplayerServer**: Implementan la funcionalidad multijugador, permitiendo que varios clientes se conecten a un servidor y compartan una simulación.
* **MonoGameGum**: Se utiliza para la interfaz de usuario (iniciar y unirse a una partida).

---

## Secuencia de Inicio

### Juego en solitario (Standalone)

1.  **Inicialización (`Game1.Initialize`)**:
    * Se inicializa el `GumService` y se carga el proyecto de la interfaz.
    * Se crea una instancia de `ElementMenu` en la pantalla principal y se suscribe a sus eventos `HostClicked` y `JoinClicked`.
    * Se calcula el tamaño de la cuadrícula basándose en las dimensiones de la ventana y el `PixelSize`.
    * Se instancian los managers principales: `GridManager`, `ControllerManager`, y `GraphicManager`.
    * Se establece un manejador para el evento `OnPlaceAction` del `ControllerManager`, que se activará cuando el usuario interactúe con la cuadrícula.
2.  **Carga de Contenido (`Game1.LoadContent`)**:
    * Se crea un `SpriteBatch` para el dibujado.
    * El `GraphicManager` carga los recursos necesarios, como la textura de 1x1 píxel que se usará para dibujar los elementos.
3.  **Ciclo de Juego (`Game1.Update` y `Game1.Draw`)**:
    * En `Update`, se actualiza la interfaz, se procesa la lógica de la cuadrícula (`_gridManager.Update`) y, si el juego está activo, se gestiona la entrada del usuario (`_controllerManager.HandleInput`).
    * En `Draw`, se limpia la pantalla, se dibujan los elementos de la cuadrícula a través del `GraphicManager` y finalmente se renderiza la UI.

### Inicio como Anfitrión (Host)

1.  El usuario introduce un puerto en la `ElementMenu` y hace clic en "Host".
2.  Se dispara el evento `HostClicked`, que llama al método `StartHost` en `Game1`.
3.  En `StartHost`:
    * Se crea una instancia de `MultiplayerServer`, pasándole el puerto y una referencia al `GridManager`.
    * Se inicia la escucha de clientes de forma asíncrona (`_server.StartListeningAsync()`).
    * La variable `_isGameActive` se establece en `true` y la `ElementMenu` se oculta, dando comienzo a la simulación.

### Inicio como Cliente

1.  El usuario introduce una dirección IP y un puerto en la `ElementMenu` y hace clic en "Join".
2.  Se dispara el evento `JoinClicked`, que llama al método `StartClient` en `Game1`.
3.  En `StartClient`:
    * Se crea una instancia de `MultiplayerClient`, pasándole una referencia al `GridManager`.
    * Se conecta al servidor especificado (`_client.Connect(ip, port)`) y se inicia la escucha de mensajes del servidor de forma asíncrona.
    * `_isGameActive` se establece en `true` y la `ElementMenu` se oculta.

---

## Arquitectura de Elementos

La simulación se basa en un sistema de elementos, donde cada "píxel" en la cuadrícula es un objeto `Element`.

* **Clase Base `Element`**: Es una clase abstracta que define las propiedades y comportamientos comunes a todos los elementos:
    * **Color**: El color con el que se dibujará el elemento.
    * **Update(GridManager.ElementAPI api, GameTime delta)**: Método abstracto que contiene la lógica de comportamiento de cada elemento (ej. cómo cae la arena, cómo fluye el agua).
    * **Interact(...)**: Método virtual para definir interacciones entre elementos.

* **Método `Update`**: Este método es el corazón de la arquitectura. Cada tipo de partícula (arena, agua, piedra) tiene su propia versión de `Update` donde se define su lógica única:
    * La **arena** implementa la gravedad para caer.
    * El **agua** implementa la caída y además la capacidad de fluir hacia los lados.
    * La **piedra** tiene un `Update` vacío porque no hace nada.

* **API de Interacción**: Cuando el juego llama al `Update` de un elemento, le pasa una herramienta (`api`) que le permite "ver" su entorno y "actuar", por ejemplo, para comprobar si la celda de abajo está vacía y moverse a ella.

---

## Arquitectura de Interfaz

La interfaz de usuario se gestiona a través de la librería **MonoGameGum**.

* **GumService**: Un servicio central que se inicializa en `Game1` y gestiona el ciclo de vida de la UI (actualización y dibujado).
* **ElementMenu**: Es una `Screen` de Gum que representa el menú principal. Su lógica se encuentra en `ElementMenu.cs`, donde se definen los eventos `HostClicked` y `JoinClicked` que se disparan al interactuar con los botones de la UI. Estos eventos son clave para desacoplar la UI de la lógica principal del juego en `Game1`.

---

## Arquitectura de Gráficos

El renderizado de la simulación es manejado por la clase `GraphicManager`.

* **Optimización**: Para dibujar la gran cantidad de partículas, se utiliza una técnica eficiente:
    1.  En `LoadContent`, se crea una única textura de 1x1 píxel de color blanco (`_pixelTexture`).
    2.  En el método `Draw`, se itera sobre cada celda de la cuadrícula (`gridManager.GetElement(x, y)`).
    3.  Si la celda no está vacía, se dibuja la textura `_pixelTexture` en la posición correspondiente, escalada por `pixelSize` y tintada con el color del elemento (`element.Color`).
* **Coordenadas**: Se realiza una inversión del eje Y para que el origen (0,0) de la cuadrícula esté en la esquina inferior izquierda, lo que es más intuitivo para una simulación de gravedad.

---

## Arquitectura de Red

La funcionalidad multijugador se construye utilizando las librerías nativas de .NET para la comunicación y una librería externa muy popular para la serialización de datos.

### Tipo de Conexión: UDP

* **Implementación**: La comunicación entre el cliente y el servidor se realiza exclusivamente a través del protocolo **UDP** (User Datagram Protocol). Esto se confirma por el uso de la clase `UdpClient` tanto en `MultiplayerServer.cs` como en `MultiplayerClient.cs`.
* **¿Por qué UDP?**: UDP es un protocolo "sin conexión". A diferencia de TCP, no garantiza que los paquetes lleguen o que lleguen en orden. Esto lo hace mucho más rápido, ya que no hay sobrecarga de confirmaciones. Para un juego de simulación en tiempo real como este, la velocidad es más importante que la fiabilidad de cada paquete individual.

### Estructura y Tipos de Paquetes

Todos los datos enviados por la red se empaquetan dentro de una clase contenedora: `NetworkMessage`. Esta clase se serializa a JSON antes de ser enviada.

* **Estructura de `NetworkMessage`**:
    * `MessageType`: Un enumerador (`enum`) que indica el propósito del mensaje.
    * `Payload`: Una cadena de texto (`string`) que contiene otro objeto serializado en JSON con los datos específicos del mensaje.

* **Tipos de Paquetes (`MessageType`)**:

    * **Handshake**
        * **Cuándo se envía**: Lo envía un cliente justo después de conectarse al servidor.
        * **Propósito**: Sirve para que el servidor registre la dirección del nuevo cliente en su lista.
        * **Payload**: Generalmente vacío.

    * **GridState**
        * **Cuándo se envía**: Lo envía el servidor a un cliente únicamente después de recibir el `Handshake` de ese cliente.
        * **Propósito**: Sincronizar al nuevo jugador. El Payload contiene una serialización completa de toda la cuadrícula (`GridState`).

    * **PlaceAction**
        * **Cuándo se envía**:
            1.  Un cliente lo envía al servidor cada vez que el jugador hace clic.
            2.  El servidor lo reenvía (broadcast) a todos los demás clientes.
        * **Propósito**: Notifica a los demás jugadores sobre las interacciones para mantener todo sincronizado en tiempo real.
        * **Payload**: Contiene un objeto `PlaceAction` serializado (coordenadas, radio, tipo de elemento).

---

## Arquitectura de Controladores

La clase `ControllerManager` se encarga de traducir las acciones del usuario en eventos dentro del juego.

* **`HandleInput`**: Este método se llama en cada fotograma desde `Game1.Update` y orquesta la gestión de entradas.
* **Entrada de Teclado (`HandleKeyboard`)**:
    * Detecta teclas numéricas para cambiar el `SelectedElementType` (el tipo de elemento a colocar).
    * Maneja la tecla `Shift` para activar el modo de reemplazo (`_isReplacing`).
* **Entrada de Ratón (`HandleMouse`)**:
    * **Clic Izquierdo**: Crea una `PlaceAction` con el elemento seleccionado. Esta acción se encola en el `GridManager` y se notifica a través del evento `OnPlaceAction`.
    * **Clic Derecho**: Similar, pero siempre con el elemento `Empty` y en modo de reemplazo para borrar.
* **`OnPlaceAction`**: Es un evento (`Action<PlaceAction>`) que se invoca cada vez que el usuario realiza una acción. En `Game1`, este evento se usa para enviar la acción al cliente o servidor multijugador, asegurando que las acciones locales se propaguen a otros jugadores.