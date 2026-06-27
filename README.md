# 🐾 CryptidCare — Containment Division

> An internal admin dashboard for managing cryptid containment operations — including entity tracking, enclosure management, keeper assignments, and feeding schedules.
Developed as part of SoftITo Backend Engineering training as Project #8.

---

## 📸 Screenshots

### Dashboard
*Central command overview: cryptid count, enclosure capacity, keeper hazard pay leaderboard, and breach alerts.*
<img width="3069" height="1917" alt="8" src="https://github.com/user-attachments/assets/991258e6-ecad-46ac-b1a8-41dd9ba7b262" />


### Global Search
*Live cross-entity search returning cryptid, enclosure, and keeper matches in a single query.*
<img width="3069" height="1917" alt="2" src="https://github.com/user-attachments/assets/9a35f8b5-0c01-4173-871a-5f290b5252cc" />


### New Containment Entry
*Register a new cryptid with nickname, species type, assigned enclosure, and entity image.*
<img width="3069" height="1917" alt="4" src="https://github.com/user-attachments/assets/f6015020-2d3d-4219-aa8f-0cfd83611464" />


### Amend Record — Keeper
*Update keeper personnel details including specialty and hazard pay rate. Restricted expunge action requires confirmation.*
<img width="3069" height="1917" alt="7" src="https://github.com/user-attachments/assets/0be9382d-ccae-4608-b140-5326aa630735" />


### Edit Cryptid Record
*Modify containment entries with live image preview. Restricted expunge panel visible to authorized users.*
<img width="3069" height="1917" alt="3" src="https://github.com/user-attachments/assets/b1c1248e-0fae-47b4-90b5-a658eba291fa" />


### Delete Confirmation Modal
*Danger-action confirmation dialog with thematic copy. "The void does not return what it takes."*
<img width="3069" height="1914" alt="5" src="https://github.com/user-attachments/assets/404b22e7-39f9-4f34-be8f-1c883e062200" />


### Print / PDF Export
*Feeding schedule print view with clean tabular layout, printable via browser or exportable as PDF.*
<img width="3069" height="1917" alt="6" src="https://github.com/user-attachments/assets/d510c8c7-360d-4722-bf9d-510e1383915c" />


### Empty Search State
*Graceful empty state when no records match the query.*
<img width="3069" height="1917" alt="1" src="https://github.com/user-attachments/assets/43276f2e-8e09-43e8-8d7e-756e35602adc" />


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

## ✨ Features

**Control Panel**
- Dashboard with live stats: cryptids contained, active enclosures, keepers on duty, feeding schedules
- Containment breach alert banner with direct navigation to affected enclosures
- Enclosure capacity bar chart
- Hazard pay leaderboard for keepers
- Recently added cryptids feed

**Cryptid Registry**
- Full CRUD: add, view, edit, and expunge cryptid records
- Fields: nickname, species type, assigned enclosure, entity image (path or URL)
- Image preview on entry and edit forms
- Restricted delete with confirmation step

**Enclosure Management**
- Track biome type, capacity, and containment status
- Status badges: Active, Breach, Offline
- Manage enclosure assignments directly from the cryptid form

**Keeper Management**
- Personnel records with specialty, hazard pay rate, and registry ID
- Edit and expunge workflows with restricted action gating

**Feeding Schedules**
- Tabular schedule list linking cryptids to keepers and dietary items
- Filter by status (active / inactive)
- Print-friendly layout for physical posting; exports as PDF or XML

**Global Search**
- Single search bar queries across cryptids, enclosures, and keepers simultaneously
- Results grouped by entity type with direct registry links
- Graceful empty state for zero-match queries

---

## 🗄️ Database

All data access goes through **Dapper** calling **T-SQL stored procedures** — no raw query strings in application code. The relational schema covers:

- `Cryptids` — entity registry
- `Enclosures` — containment zones with capacity and status
- `Keepers` — personnel with specialty and pay rate
- `FeedingSchedules` — many-to-many linking cryptids, keepers, and dietary items

Stored procedures handle all CRUD operations, search, and aggregate queries (dashboard stats, capacity rollups, leaderboard).

---

## 🔐 Authentication *(Planned)*

Login and role-based access control will be implemented using **ASP.NET Core Identity**. Planned roles:

- **Viewer** — read-only access to all registries
- **Keeper** — can manage feeding schedules and view assigned cryptids
- **Admin** — full CRUD including restricted expunge actions

---

## 📤 Exports

Listing views (Feeding Schedules, Cryptid Registry, Keepers) support:

- **PDF** — browser print dialog with print-optimized stylesheet; also exportable via server-side PDF generation
- **XML** — structured data export for system integrations or archiving

---

## 🚀 Getting Started

```bash
# Clone the repo
git clone https://github.com/zaruyashar/cryptidcare.git
cd cryptidcare

# Restore dependencies
dotnet restore

# Apply database migrations / run seed scripts
# (T-SQL scripts located in /Database/Scripts)
sqlcmd -S localhost -d CryptidCare -i Database/Scripts/schema.sql
sqlcmd -S localhost -d CryptidCare -i Database/Scripts/seed.sql

# Configure connection string in appsettings.json
# "ConnectionStrings": { "DefaultConnection": "Server=...;Database=CryptidCare;..." }

# Run the application
dotnet run
```
