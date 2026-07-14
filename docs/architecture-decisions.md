# Architecture Decisions - Mundesi per Rinine

Data: 2026-07-14

Ky dokument mban vendimet arkitekturore fillestare. Vendimet mund te ndryshojne, por cdo ndryshim duhet te dokumentohet me arsye.

## ADR-001 - Modular Monolith me Clean Architecture te moderuar

Status: Proposed

Vendim:

Te perdoret Modular Monolith, i ndare ne `Api`, `Application`, `Domain` dhe `Infrastructure`, pa microservices per MVP.

Arsye:

- SRS kerkon shume module, por jo shkallezim operacional qe justifikon microservices.
- Modular Monolith jep ndarje te qarte per testim dhe mirembajtje.
- E ul kompleksitetin e deployment-it per nje projekt 5-mujor.

Pasojat:

- Duhet disipline ne dependencies midis projekteve.
- Domain nuk duhet te varet nga EF Core, HTTP ose Infrastructure.
- Controllers duhet te mbeten te holle.

## ADR-002 - SQLServer si databaze fillestare

Status: Proposed

Vendim:

Te perdoret SQLServer si databaze primare per MVP.

Arsye:

- SRS e jep SQLServer si zgjedhjen e pare.
- Nuk ka databaze ekzistuese ne repository qe ta kufizoje vendimin.
- SQLServer mbeshtet mire indexes, full-text search dhe JSON kur nevojitet.

Pasojat:

- Duhet Docker Compose per zhvillim lokal.
- Integration tests duhet te perdorin Testcontainers me SQLServer.
- Nese vendoset SQL Server me vone, duhet ADR i ri.

## ADR-003 - ASP.NET Core Identity per auth

Status: Proposed

Vendim:

Te perdoret ASP.NET Core Identity per perdoruesit, password hashing, lockout, email verification dhe role baseline.

Arsye:

- Eshte mekanizmi standard dhe i testuar i ASP.NET Core.
- SRS e kerkon shprehimisht.
- Ul rrezikun e gabimeve ne auth.

Pasojat:

- Duhet te zgjerohet `ApplicationUser`.
- Permissions nuk duhet te varen vetem nga roles; duhen policies/claims.
- Duhet kujdes me token storage dhe refresh token rotation.

## ADR-004 - JWT access token + HttpOnly refresh token cookie

Status: Proposed

Vendim:

Access token te kete jete te shkurter. Refresh token te ruhet ne HttpOnly, Secure, SameSite cookie dhe te rrotullohet ne cdo refresh.

Arsye:

- Ul ekspozimin ndaj XSS krahasuar me ruajtjen e refresh token ne localStorage.
- SRS kerkon refresh token rotation.

Pasojat:

- Frontend duhet te mbeshtese session state pa ruajtur secrets ne localStorage.
- Backend duhet te kete CSRF strategy per operacione state-changing nese perdoren cookies.
- Token reuse detection duhet testuar.

## ADR-005 - Policy-based authorization me permissions

Status: Proposed

Vendim:

Te perdoren policies dhe permissions, jo vetem role checks.

Permissions fillestare:

- `Opportunities.View`
- `Opportunities.Create`
- `Opportunities.EditOwn`
- `Opportunities.EditAny`
- `Opportunities.Submit`
- `Opportunities.Review`
- `Opportunities.Publish`
- `Opportunities.Reject`
- `Organizations.ManageOwn`
- `Organizations.ManageAny`
- `Users.Manage`
- `Reports.View`
- `Reports.Export`
- `System.Manage`

Arsye:

- Role si `Partner` ose `ContentManager` nuk mjaftojne per raste reale.
- Permissions jane me fleksibile per organizata dhe workflow.

Pasojat:

- Duhet nje model i qarte per roles -> permissions.
- Tests duhet te perfshijne authorization negative cases.

## ADR-006 - ProblemDetails per te gjitha gabimet API

Status: Proposed

Vendim:

Te gjitha gabimet HTTP te kthehen si ProblemDetails, me structure konsistente per validation errors.

Arsye:

- SRS e kerkon.
- Frontend mund te trajtoje gabimet ne menyre uniforme.
- E ben API-ne me te qarte per testim dhe dokumentim.

Pasojat:

- Duhet global exception handling.
- Duhet mapping i validation errors ne ProblemDetails.
- Nuk duhet te ekspozohen stack traces ne production.

## ADR-007 - Frontend feature-based structure

Status: Proposed

Vendim:

React app te organizohet sipas features, jo me nje folder global `services` per cdo gje.

Struktura e synuar:

```text
src/
  app/
    api/
    providers/
    router/
  features/
    auth/
    profile/
    opportunities/
    favorites/
    notifications/
    organizations/
    admin/
    reports/
  components/
  layouts/
  hooks/
  lib/
  types/
  i18n/
```

Arsye:

- Shmang perzierjen e moduleve.
- E ben me te lehte mentorimin dhe code review per feature.
- Perputhet me SRS.

Pasojat:

- Duhet vendosur rregull i qarte per cfare shkon ne `components` global dhe cfare ne `features`.

## ADR-008 - TanStack Query per server state

Status: Proposed

Vendim:

Te perdoret TanStack Query per data fetching, caching, invalidation dhe mutations.

Arsye:

- SRS e kerkon.
- Server state nuk duhet duplikuar ne Redux/global client state.

Pasojat:

- API client duhet te kete error handling konsistent.
- Query keys duhet te standardizohen.
- Optimistic updates duhen perdorur me kujdes vetem aty ku jane te sigurta.

## ADR-009 - Test strategy qe ne fillim

Status: Proposed

Vendim:

Te krijohen test projects ne Foundation, edhe para features te medha.

Arsye:

- Testet jane pjese e arkitektures, jo shtese ne fund.
- Auth, permissions dhe workflow do te jene te rrezikshme pa integration tests.

Pasojat:

- Setup fillestar zgjat pak me shume.
- Cdo use case duhet te kete minimalisht validator/domain/integration tests sipas riskut.

## ADR-010 - SRS si burim kryesor, por scope i kontrolluar

Status: Accepted

Vendim:

SRS eshte burimi kryesor i kerkesave, por implementimi do te behet me milestones te vogla. Nuk do te gjenerohet nje feature i plote pa pjesemarrjen e studentit.

Arsye:

- Projekti ka qellim mesimor dhe production-ready.
- Implementimi i gjithe SRS-se ne nje hap do te ishte i veshtire per t'u kuptuar, testuar dhe mirembajtur.

Pasojat:

- Cdo milestone duhet te kete teori, flow teknik, files, detyre per studentin, acceptance criteria dhe code review.
- Implementimi duhet te ndalet per review para se te kaloje ne hapat e radhes.
