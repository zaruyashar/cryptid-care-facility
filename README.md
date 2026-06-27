# 🐾 CryptidCare — Containment Division

> An internal admin dashboard for managing cryptid containment operations — including entity tracking, enclosure management, keeper assignments, and feeding schedules.
Developed as part of SoftITo Backend Engineering training as Project #8.

---

## 📸 Screenshots

### Dashboard
*Central command overview: cryptid count, enclosure capacity, keeper hazard pay leaderboard, and breach alerts.*
<img width="3069" height="1917" alt="1" src="https://github.com/user-attachments/assets/9a9c78c5-5bd5-486e-89a5-600a0fae126a" />


### Global Search
*Live cross-entity search returning cryptid, enclosure, and keeper matches in a single query.*
<img width="3069" height="1917" alt="7" src="https://github.com/user-attachments/assets/7f86ba2e-72b2-4314-a07c-d03684f08a52" />


### New Containment Entry
*Register a new cryptid with nickname, species type, assigned enclosure, and entity image.*
<img width="3069" height="1914" alt="5" src="https://github.com/user-attachments/assets/703c8d0e-c11c-4834-8e6e-9371e93fe259" />


### Amend Record — Keeper
*Update keeper personnel details including specialty and hazard pay rate. Restricted expunge action requires confirmation.*
<img width="3069" height="1917" alt="2" src="https://github.com/user-attachments/assets/1ca0a9eb-ecd7-4e8e-8630-3d3ab0ebf2a4" />


### Edit Cryptid Record
*Modify containment entries with live image preview. Restricted expunge panel visible to authorized users.*
<img width="3069" height="1917" alt="6" src="https://github.com/user-attachments/assets/5a03b2bd-0392-4217-b1e8-f77c6796f548" />


### Delete Confirmation Modal
*Danger-action confirmation dialog with thematic copy. "The void does not return what it takes."*
<img width="3069" height="1917" alt="4" src="https://github.com/user-attachments/assets/c7f9cad3-b340-48ef-b653-ca074bb5da6f" />


### Print or PDF / XML Export
*Feeding schedule print view with clean tabular layout, printable via browser or exportable as PDF.*
<img width="3069" height="1917" alt="3" src="https://github.com/user-attachments/assets/4a381e29-f13e-45da-956d-fd47cc6df63e" />
<img width="3069" height="1917" alt="9" src="https://github.com/user-attachments/assets/cee6377c-3a8f-41d3-9a41-1a4534028227" />


### Empty Search State
*Graceful empty state when no records match the query.*
<img width="3069" height="1917" alt="8" src="https://github.com/user-attachments/assets/0dbcbd02-98df-459d-bb2b-ed9fd277db6d" />


---

## 🧰 Tech Stack

| Layer | Technology |
|---|---|
| Language | C# (.NET Core) |
| ORM / Data Access | Dapper (micro-ORM) |
| Database | SQL Server — relational schema with T-SQL stored procedures |
| Auth *(planned)* | ASP.NET Core Identity (Microsoft Identity Library) |
| Export | PDF and XML downloads on listing views |
| Frontend | Server-rendered Razor views with custom CSS |

---
