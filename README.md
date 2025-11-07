# DolhofCodingGame

Dit project is een educatieve puzzle-game waarin spelers leren programmeren door een speler over een rooster te laten bewegen met behulp van commando's zoals **Forward**, **Turn Left**, **Turn Right** en **Repeat loops**.  
Het doel van het spel is om de speler naar het **kaas eindpunt** te leiden zonder tegen muren te botsen.

---

## 🎮 Spel Uitleg

De speler staat op een starttegel in een doolhof.  
De speler kan niet vrij bewegen — beweging gebeurt enkel met **commando’s** die jij invoert.

### Beschikbare Commando’s

| Commando | Beschrijving |
|---------|--------------|
| **Forward X** | Beweegt de speler X stappen vooruit |
| **Left X** | Draait de speler X keer 90° naar links |
| **Right X** | Draait de speler X keer 90° naar rechts |
| **Repeat X ... End Repeat** | Herhaalt een blok commando’s X keer |

De speler moet het einde bereiken (kaas 🧀).  
Als het niet lukt → de speler reset automatisch terug naar start en je kan opnieuw proberen.

---

## 🏁 Doel van de Game

Gebruik logica en herhaling om efficiënte oplossingen te vinden voor elk level.  
Hoe minder stappen → hoe beter!

---

## 🚀 Hoe opstarten zonder Unity

1. Download de **Build map** (via link / zip / Drive).
2. Open de map.
3. Dubbelklik op:
My Project (2).exe

**Screenshot Level 1:**
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/2b816f3a-f9eb-4de6-8b80-090d95f92c6e" />

**Oplossing Level 1:**
Forward x3
Right x1
Forward x4
Left x1
Forward x3

**Screenshot Level 2:**
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/e94261fc-7d49-42f6-967b-dd7b6052497f" />

**Oplossing Level 2:**
Repeat x4
Forward x8
Right x1
End Repeat

**Screenshot Level 3:**
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/67bbd33e-e690-440e-908b-76ce132f2510" />

**Oplossing Level 3:**
Forward x10
Right x1
Forward x2
Left x1
Forward x6
Right x1
Forward x5
Left x1
Forward x2
Right x1
Forward x15
Right x1
Repeat x2
Forward x4
Right x1
End Repeat
Forward x2
Left x1
Forward x4
Left x1
Forward x2
Right x1
Repeat x2
Forward x8
Left x1
End Repeat
Forward x2

**Kortere oplossing om gewoon te testen level 3:**
Right x1
Forward x10
Left x1
Forward x10

## 👨‍💻 Code Structuur

| Folder | Inhoud |
|--------|--------|
| `Assets/Scripts` | Code voor player movement & command execution |
| `Assets/Tilemaps` | Maze en tiles |
| `Assets/UI` | Gebruikersinterface elementen |
| `Build/` | Speelbare .exe bestand |

De **Library, Temp, Obj, Build** mappen worden **niet** meegestuurd naar GitHub (Unity genereert deze zelf).

---

## 📦 Technologieën

- Unity (C#)
- Tilemap systeem
- UI met TextMeshPro
- Coroutines voor movement & acties
- GitHub versiebeheer

---

## ✍️ Gemaakt door

**Naam:** Raihane Benjilali    
**School:** Erasmushogeschool

---

Veel succes & veel plezier! 🎉🧀
