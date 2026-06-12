# Bug 1 — Los endpoints de scoring devuelven 403 Forbidden para usuarios con roles IPT

## Cómo encontré el bug

Revisé el comportamiento del sistema al acceder a un WSM con un usuario con roles IPT. Durante la revisión identifiqué el siguiente bug:

**Título del bug:** Los 6 endpoints de cálculo de scores del backend PointScore devuelven 403 Forbidden para todos los usuarios con roles IPT, impidiendo que la página de detalle del WSM cargue correctamente para esos usuarios.

**Descripción del bug:** Al iniciar sesión con un usuario que tenía roles IPT (como IPTLogistics, IPTSafety, IPTSoftware, IPTQuality) y acceder al detalle de cualquier WSM, la consola del navegador mostraba 5 errores consecutivos de `Forbidden` provenientes de los endpoints de cálculo de scores técnico, funcional, de usuario, de schedule y de costo. La página cargaba vacía o incompleta para estos usuarios a pesar de que el usuario tenía una licencia válida y acceso correcto al sistema.

---

## Cómo lo resolví

- Identifiqué que los errores eran de tipo 403 Forbidden y no 401 Unauthorized, lo que indicaba que el token se validaba correctamente pero la autorización a nivel de endpoint fallaba.
- Verifiqué que el middleware de licencia no era el causante porque tiene configurado un bypass explícito para la ruta `/api/wsm-feature`.
- Inspeccioné los 6 endpoints de scoring en el controlador de PointScore y encontré que todos tenían el atributo de autorización restringido únicamente a los roles Admin, ProgramManager y ConfigurationManager.
- Determiné que estos endpoints son de solo lectura y que cualquier usuario con acceso válido al WSM debería poder consultarlos para visualizar los scores en la página de detalle.
- Amplié la lista de roles permitidos en los 6 endpoints para incluir WSMOwner, BlockOwner y todos los roles IPT del sistema.
- Confirmé que tras el fix los errores de Forbidden desaparecieron y la página de detalle del WSM cargaba correctamente para los usuarios IPT.
