# Current State Analysis - Mundesi per Rinine

Data: 2026-07-14

Burimet e analizuara:

- Repository lokal: `C:\Users\Admin\source\repos\arber-halimi\mundesi-per-rinine`
- SRS: `C:\Users\Admin\Downloads\SRS_Mundesi_per_Rinine_DotNet_React.pdf`, versioni 1.0, korrik 2026
- Kerkesat e ngjitura nga perdoruesi per menyren e mentorimit dhe stack-un teknik

## A. Current State

Repository eshte ne faze fillestare. Nuk ka ende solution .NET, projekt React, databaze, test projects, Docker, CI/CD ose dokumentacion te projektit.

Gjendja aktuale e files/folders:

- `.git/` ekziston.
- `.vs/` ekziston dhe eshte e pa-trackuar nga Git.
- Nuk ekziston `src/`.
- Nuk ekziston `tests/`.
- Nuk ekziston `docs/` para kesaj analize.
- SRS nuk ndodhet ne repository; ndodhet ne `Downloads`.

Ambient lokal i verifikuar:

- .NET SDK: 10.0.301
- ASP.NET Core runtime: 10.0.9 dhe 6.0.36
- Node.js: v24.14.0
- pnpm: 11.7.0
- `global.json` nuk ekziston ende.

Konkluzion: projekti eshte ne nivel "pre-implementation/discovery". Nuk ka ende kod per te vleresuar stilin, maturine arkitekturore, sigurine ose testueshmerine.

## B. Implemented Features

Nuk ka feature te implementuara ende.

Nga pikepamja e SRS-se, keto fusha jane ende 0% te implementuara:

- Autentikimi dhe llogaria
- Profili dhe preferencat
- Katalogu publik i mundesive
- Kerkimi dhe filtrimi
- Favorites
- Reminders
- Partner portal
- Review workflow
- Admin dashboard
- Raportimi
- Shumegjuhesia
- Feedback dhe raportimi i permbajtjes se pasakte
- Testing, DevOps dhe observability

## C. Missing Features

Sipas SRS-se mungon e gjithe baza e MVP-se:

- Solution .NET me ndarje `Api`, `Application`, `Domain`, `Infrastructure`.
- React + TypeScript + Vite frontend.
- Databaze relacionale me EF Core migrations.
- ASP.NET Core Identity, JWT access token dhe refresh token rotation.
- Policy-based authorization dhe permissions.
- Global exception handling me ProblemDetails.
- FluentValidation.
- Serilog dhe correlation ID.
- OpenAPI/Swagger.
- Docker Compose per databazen lokale.
- Test projects per unit, integration dhe architecture tests.
- Strukturim frontend sipas features.
- API client, routing, layout dhe environment configuration.

## D. Technical Debt

Teknikisht nuk ka ende debt nga kodi, sepse kodi nuk ekziston. Ka disa pika organizative qe duhen adresuar heret:

- `.vs/` eshte e pa-trackuar; duhet te shtohet `.gitignore` para se te filloje puna serioze.
- SRS eshte jashte repository-t; duhet vendosur nje kopje ne `docs/` ose te dokumentohet qarte si burim i jashtem.
- Nuk ka `README.md` per setup lokal.
- Nuk ka vendim te dokumentuar per databazen perfundimtare.
- Nuk ka `global.json` per te fiksuar SDK-ne e .NET.
- Nuk ka standard per branch, commit, naming dhe testim.

## E. Security Risks

Rreziqet kryesore jane te lidhura me vendimet qe duhet te merren para implementimit:

- Projekti do te punoje me perdorues 15-29 vjec; per grupmoshen 15-17 vjec duhet minimizim i te dhenave dhe politike e qarte privatësie.
- Auth duhet te shmange ruajtjen e JWT ne `localStorage` kur perdoren refresh tokens.
- Refresh token duhet te jete HttpOnly, Secure dhe SameSite cookie.
- CORS duhet te lejoje vetem origin-et e njohura.
- File upload per media duhet te kete validim MIME/extension/size dhe hook per malware scan.
- Admin/partner endpoints nuk duhet te mbeshteten vetem ne role; duhet policy/permissions.
- Audit logs nuk duhet te ruajne passwords, tokens ose te dhena sensitive.
- Rate limiting duhet perfshire heret per auth, search, reports dhe upload.

## F. Proposed Architecture

Rekomandohet Modular Monolith me Clean Architecture te moderuar, si ne SRS, pa microservices.

Struktura fillestare e propozuar:

```text
src/
  YouthOpportunities.Api/
  YouthOpportunities.Application/
  YouthOpportunities.Domain/
  YouthOpportunities.Infrastructure/
web/
  youth-opportunities-web/
tests/
  YouthOpportunities.UnitTests/
  YouthOpportunities.IntegrationTests/
  YouthOpportunities.ArchitectureTests/
docs/
```

Pergjegjesite:

- `Api`: controllers/endpoints, middleware, auth setup, OpenAPI, ProblemDetails.
- `Application`: use cases, DTO, validation, authorization checks, query services.
- `Domain`: entities, enums, value objects, domain rules.
- `Infrastructure`: EF Core, Identity persistence, email, Hangfire, storage, external services.
- `web`: React app me feature-based structure.
- `tests`: testim i ndare sipas nivelit.

Databaza e propozuar per fillim: PostgreSQL, sepse SRS e jep si zgjedhjen e pare dhe nuk ka databaze ekzistuese qe ta kufizoje vendimin.

## G. Implementation Roadmap

Roadmap i nivelit te larte:

1. Foundation
   - Scaffolding i solution-it dhe frontend-it.
   - `.gitignore`, `README`, `global.json`.
   - Docker Compose per PostgreSQL.
   - EF Core, Identity, ProblemDetails, logging, OpenAPI.

2. Identity dhe Profile
   - Register, verify email, login, refresh, logout.
   - Current user, profile, interests, notification preferences.

3. Opportunity Core
   - Categories, organizations, municipalities, opportunities.
   - Status workflow dhe review history.

4. Public Experience
   - Catalog, search, filters, details, application click tracking.

5. User Features
   - Favorites, notifications, reminders, content report.

6. Partner Portal
   - Partner organization membership, draft opportunities, submit for review.

7. Administration
   - Review queue, approvals/rejections, category/org management, audit logs.

8. Reporting
   - KPI dashboard, CSV/XLSX/PDF exports.

9. Quality and Release
   - Unit/integration/E2E tests, accessibility, performance, security review, backup/restore.

## H. First Development Milestone

Milestone i pare i pershtatshem: Foundation Skeleton.

Qellimi nuk eshte te implementohet auth i plote ende. Qellimi eshte te krijohet nje baze e paster ku cdo feature e ardhshme ka vendin e vet.

Scope i milestone-it:

- Krijo strukturën e solution-it .NET.
- Krijo React app me Vite/TypeScript.
- Shto `.gitignore`, `README.md`, `global.json`.
- Shto Docker Compose per PostgreSQL.
- Shto projektet testuese bosh.
- Konfiguro minimalisht API health endpoint dhe OpenAPI.
- Dokumento vendimet fillestare arkitekturore.

Detyra e pare 30-90 minuta per studentin:

- Krijo `.gitignore`, `global.json`, `README.md` dhe strukturen bosh `src/`, `tests/`, `web/`.
- Mos implemento feature.
- Qellimi eshte organizimi profesional i repository-t.

Kriteret e pranimit:

- `.vs/` nuk shfaqet me si file/folder i pa-trackuar pasi shtohet `.gitignore`.
- `global.json` fikson .NET SDK major/minor te perdorur nga projekti.
- `README.md` shpjegon shkurt qellimin e projektit dhe prerequisitet.
- Folderat `src/`, `tests/`, `web/`, `docs/` ekzistojne.
- `git status --short` tregon vetem files qe realisht duam t'i commit-ojme.

Pyetje bllokuese perpara implementimit te plote:

- A do te perdoret PostgreSQL si vendim perfundimtar per MVP?
- A duhet SRS-ja te kopjohet ne `docs/` apo te mbahet vetem si dokument i jashtem?
- A do te kete login social ne MVP apo vetem email/password?
- Cila politike do te ndiqet per perdoruesit 15-17 vjec?
