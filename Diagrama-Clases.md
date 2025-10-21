```plantuml
@startuml ola
class Game1 {
    + Game1()
    + StartHost(port:int) : void
    + StartClient(ip:string, port:int) : void
}
Game <|-- Game1
class ControllerManager {
    + Radius : int <<get>> <<set>> = 5
    + ControllerManager(gridManager:GridManager, pixelSize:int)
    + HandleInput(time:GameTime) : void
}
class "Action`1"<T> {
}
ControllerManager --> "OnPlaceAction<PlaceAction>" "Action`1"
ControllerManager --> "SelectedElementType" Type
class PlaceAction {
    + radius : int <<get>>
    + isReplacing : bool <<get>>
}
PlaceAction o-> "position" Vector2I
PlaceAction o-> "elementType" Type
class Border {
    + <<override>> Clock : byte <<get>> <<set>>
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Border
Border --> "Instance" Border
abstract class Element {
    + <<virtual>> Clock : byte <<get>> <<set>> = 0
    + {abstract} Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<virtual>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element --> "Color" Color
class Empty <<sealed>> {
    + <<override>> Clock : byte <<get>> <<set>>
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Empty
Empty --> "Instance" Empty
abstract class KineticElement {
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- KineticElement
KineticElement --> "Velocity" Vector2
class GraphicManager {
    + LoadContent(device:GraphicsDevice) : void
    + Draw(spriteBatch:SpriteBatch) : void
    + {static} GetGridSize(graphics:GraphicsDeviceManager, pixelSize:int) : (int rows, int columns)
}
class GridManager {
    + Width : int <<get>>
    + Height : int <<get>>
    + Generation : byte <<get>> = 0
    + GridManager(width:int, height:int)
    + Clear() : void
    + Update(delta:GameTime) : void
    + IsInBounds(x:int, y:int) : bool
    + IsInBounds(position:Vector2I) : bool
    + GetElement(x:int, y:int) : Element
    + SetElement(x:int, y:int, element:Element) : void
    + EnqueueAction(action:PlaceAction) : void
    + GetGridState() : GridState
    + SetGridState(gridState:GridState) : void
}
struct InteractionAPI {
    + GetElement(offsetX:int, offsetY:int) : Element
}
struct ElementAPI {
    + GetElement(offsetX:int, offsetY:int) : Element
    + SwapWith(offsetX:int, offsetY:int) : void
    + MoveTo(offsetX:int, offsetY:int) : void
    + SetElement(offsetX:int, offsetY:int, element:Element) : void
}
GridManager +-- InteractionAPI
GridManager +-- ElementAPI
class GridState {
}
class ElementInfo {
    + X : int <<get>> <<set>>
    + Y : int <<get>> <<set>>
}
class "List`1"<T> {
}
GridState --> "Elements<ElementInfo>" "List`1"
ElementInfo --> "ElementType" Type
class MultiplayerClient {
    + MultiplayerClient(gridManager:GridManager)
    + Connect(ipAddress:string, port:int) : void
    + <<async>> SendActionAsync(action:PlaceAction) : Task
    + <<async>> StartListeningAsync() : Task
    + Dispose() : void
}
IDisposable <|-- MultiplayerClient
class MultiplayerServer {
    + MultiplayerServer(port:int, gridManager:GridManager)
    + <<async>> StartListeningAsync() : Task
    + <<async>> BroadcastActionAsync(action:PlaceAction, origin:IPEndPoint?) : Task
    + Dispose() : void
}
IDisposable <|-- MultiplayerServer
enum MessageType {
    PlaceAction,
    GridState,
    Handshake,
}
class NetworkMessage {
    + Payload : string <<get>> <<set>>
}
NetworkMessage --> "MessageType" MessageType
class RandomProvider <<static>> {
}
RandomProvider o-> "Random" Random

class "IEquatable`1"<T> {
}
"IEquatable`1" "<Vector2I>" <|-- Vector2I
Vector2I --> "Zero" Vector2I
Vector2I --> "One" Vector2I
Vector2I --> "UnitX" Vector2I
Vector2I --> "UnitY" Vector2I
class Smoke {
    + Smoke()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Smoke
class Steam {
    + Steam()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Steam
class Acid {
}
class Blood {
    + Blood()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + MDispertion() : int
    + BloodPattern() : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Blood
class Water {
    + Dispertion : int <<get>> <<set>>
    + Water()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
    + MDispertion() : int
    + WaterPattern() : void
}
Element <|-- Water
class Dirt {
    + Dirt()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Dirt
class KineticSolid {
    + KineticSolid(color:Color)
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- KineticSolid
class Sand {
    + Sand()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Sand
class WomenCum {
}
class Flesh {
    + Flesh()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Flesh
class Stone {
    + Stone()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Stone
class Wood {
    + Wood()
    + <<override>> Update(api:GridManager.ElementAPI, delta:GameTime) : void
    + <<override>> Interact(interactionApi:GridManager.InteractionAPI, elementApi:GridManager.ElementAPI) : void
}
Element <|-- Wood
@enduml
