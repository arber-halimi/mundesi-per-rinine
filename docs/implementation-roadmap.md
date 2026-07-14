# Implementation Roadmap - Mundesi per Rinine

Data: 2026-07-14

Ky roadmap e kthen SRS-ne ne hapa te vegjel pune. Projekti duhet te zhvillohet gradualisht, me mentorim, code review dhe verifikim pas cdo hapi.

## Faza 1 - Foundation

Qellimi: te krijohet baza teknike e projektit para se te shkruhen feature.

Detyra 30-90 minuta:

1. Repository hygiene
   - `.gitignore`
   - `README.md`
   - `global.json`
   - folderat `src`, `tests`, `web`, `docs`

2. Backend skeleton
   - `YouthOpportunities.Api`
   - `YouthOpportunities.Application`
   - `YouthOpportunities.Domain`
   - `YouthOpportunities.Infrastructure`
   - solution file
   - project references

3. Test skeleton
   - `YouthOpportunities.UnitTests`
   - `YouthOpportunities.IntegrationTests`
   - `YouthOpportunities.ArchitectureTests`

4. API baseline
   - health endpoint
   - OpenAPI
   - ProblemDetails baseline
   - CORS config placeholder

5. Database baseline
   - Docker Compose per PostgreSQL
   - EF Core packages
   - `ApplicationDbContext`
   - initial migration vetem pasi modeli minimal te jete i qarte

6. Frontend skeleton
   - React + TypeScript + Vite
   - React Router
   - TanStack Query provider
   - feature folder structure
   - environment config

Kriteret e perfundimit:

- Backend build kalon.
- Frontend build kalon.
- Test projects ekzekutohen edhe nese kane vetem teste minimale.
- API hapet lokalisht dhe shfaq OpenAPI.
- Dokumentimi i setup-it lokal eshte i mjaftueshem per nje zhvillues tjeter.

## Faza 2 - Identity dhe Profile

Qellimi: perdoruesi mund te krijoje llogari, te hyje dhe te menaxhoje profilin bazik.

Use cases:

- UC-01 Regjistrimi i perdoruesit
- UC-02 Login dhe refresh token
- UC-03 Menaxhimi i profilit dhe interesave

Backend:

- ASP.NET Core Identity
- `ApplicationUser`
- `RefreshToken`
- `UserProfile`
- `Interest`
- `UserInterest`
- `NotificationPreference`
- JWT access token
- HttpOnly refresh token cookie
- FluentValidation
- rate limiting per auth

Frontend:

- `/register`
- `/login`
- `/profile`
- auth provider
- protected routes
- React Hook Form + Zod
- TanStack Query mutations

Acceptance criteria:

- Duplicate email refuzohet me ProblemDetails.
- Login i gabuar nuk zbulon nese email ekziston.
- Refresh token rotation funksionon.
- Logout revokon refresh token.
- Profili mund te perditesohet vetem nga perdoruesi i autentikuar.

## Faza 3 - Opportunity Core

Qellimi: modeli kryesor i mundesive dhe workflow i publikimit.

Use cases:

- UC-08 Partneri propozon nje mundesi
- UC-09 Review dhe publikimi
- UC-10 Kategorite dhe organizatat

Entitete:

- Category
- Municipality
- Organization
- OrganizationMember
- Opportunity
- OpportunityTranslation
- OpportunityStatusHistory
- OpportunityReview
- MediaFile
- AuditLog
- OutboxMessage

Acceptance criteria:

- Partneri krijon Draft vetem per organizaten e vet.
- Draft mund te kaloje ne PendingReview.
- Manager/Admin mund te aprovoje, refuzoje ose kerkoje ndryshime.
- Published opportunities shfaqen publikisht.
- Status transitions testohen.

## Faza 4 - Public Experience

Qellimi: vizitori gjen mundesi shpejt dhe qarte.

Use cases:

- UC-04 Shfletimi, kerkimi dhe filtrimi
- UC-05 Detajet dhe klikimi per aplikim
- UC-13 Shumegjuhesia bazike

Backend:

- `GET /api/v1/opportunities`
- `GET /api/v1/opportunities/{slug}`
- pagination metadata
- `AsNoTracking`
- indexes per filters
- tracking per views/clicks

Frontend:

- catalog page
- filter panel
- details page
- loading/error/empty states
- URL-synced filters

Acceptance criteria:

- Kerkimi dhe filtrimi behen ne backend.
- Rezultatet jane te paginuara.
- Vetem Published opportunities jane publike.
- Application click ruhet pa bllokuar navigimin.

## Faza 5 - User Features

Qellimi: perdoruesi ruan mundesi dhe merr njoftime relevante.

Use cases:

- UC-06 Favorites
- UC-07 Reminders
- UC-11 Raportimi i informacionit te pasakte

Acceptance criteria:

- Favorite PUT/DELETE jane idempotent.
- Unique constraint `UserId + OpportunityId`.
- Reminder nuk dergohet dy here per te njejtin threshold.
- Content report ka rate limiting dhe nuk ekspozon te dhena sensitive.

## Faza 6 - Partner Portal

Qellimi: partneret menaxhojne draftet e tyre.

Features:

- organization profile
- member permissions
- own opportunities table
- multi-step opportunity form
- submit for review
- status history

Acceptance criteria:

- Partneri nuk sheh ose ndryshon mundesite e organizatave te tjera.
- Upload ka validim.
- Draft ruhet pa publikuar permbajtje.

## Faza 7 - Administration

Qellimi: stafi kontrollon cilesine dhe sigurine e permbajtjes.

Features:

- review queue
- approve/reject/request changes
- user management
- role/permission management
- organization verification
- content reports
- audit log viewer

Acceptance criteria:

- Cdo veprim kritik ka audit log.
- Authorization eshte policy-based.
- Admin endpoints kane integration tests.

## Faza 8 - Reporting

Qellimi: platforma mat perdorimin dhe ndikimin.

Features:

- views
- favorites
- application clicks
- overview dashboard
- exports CSV/XLSX/PDF
- background export jobs

Acceptance criteria:

- Raportet filtrohen sipas periudhes.
- Export ka permission dhe idempotency.
- Te dhenat personale minimizohen.

## Faza 9 - Quality and Release

Qellimi: MVP te jete i qendrueshem per publikim.

Checklist:

- unit tests
- integration tests
- Playwright E2E
- accessibility checks
- dependency scan
- OWASP ZAP baseline
- performance smoke test
- Docker image build
- CI/CD
- backup dhe restore test
- production checklist

## Milestone i pare i zgjedhur

Milestone: Foundation Skeleton.

Detyra e pare per studentin:

Krijo hygiene files dhe strukturen fillestare te repository-t:

- `.gitignore`
- `global.json`
- `README.md`
- folderat `src/`, `tests/`, `web/`, `docs/`

Mos krijo ende controllers, entities ose React components. Ky hap meson disiplinen baze te nje repository profesional.

Branch i propozuar:

- `codex/foundation-skeleton`

Commit message i propozuar:

- `chore: add initial project structure`
