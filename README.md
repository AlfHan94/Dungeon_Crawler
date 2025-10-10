# 🏰 Dungeon Crawler – Labb 2 (ITHS)

Ett konsolbaserat “dungeon crawler”-spel utvecklat i C# som en del av kursen i Objektorienterad Programmering.  
Spelet använder arv, abstrakta klasser och inkapsling för att bygga en spelvärld med väggar, spelare och fiender.

---

## 🎮 Funktioner
- Läsning av bana från textfil (`Level1.txt`)  
- Objektorienterad struktur:  
  - **LevelElement** (abstrakt basklass)  
  - **Wall**, **Enemy**, **Rat**, **Snake**, **Player**  
- Spelaren kan röra sig, attackera fiender och försvara sig  
- Slumpmässigt rörliga råttor och ormar med unika stats  
- Synfält med utforskningseffekt (radie 5)  
- Combat-logg med tydlig feedback och färgade meddelanden  
- Fiender försvinner när de besegras  
- En textbaserad slutsekvens visas när spelaren dör  

---

## ⚙️ Kör spelet
1. Öppna projektet i Visual Studio  
2. Se till att `Level1.txt` ligger i mappen `Levels/`  
3. Kör programmet (`Ctrl + F5`)  
4. Styr med piltangenterna, utforska och slåss!  

---

## 📘 Teknik & Fokus
- Arv och abstraktion i C#  
- Hantering av listor och objekt  
- Enkel spel-loop och kollisioner  
- Simulerade tärningskast för stridssystem  

---

### 👨‍💻 Utvecklad av Alfred Handin 
ITHS – Labb 2, Objektorienterad Programmering
