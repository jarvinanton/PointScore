# Revisión QA — Acceso IPT al Detailed WSM Assessment

## Problema reportado por el cliente

El cliente reportó que los usuarios con roles IPT (Logistics, Quality, Safety, Software) no podían acceder al bloque de Detailed SIA dentro del Detailed WSM Assessment, ni a la mayoría de los otros items del workflow de la Fase 3. El cliente indicó que había probado con los roles IPT Logistics, Quality, Safety y Software y ninguno tenía acceso funcional a esos items.

---

## Análisis y trabajo realizado

A partir del reporte del cliente inicié la revisión del acceso de usuarios IPT al detalle del WSM, siguiendo el flujo desde el inicio de sesión hasta la apertura del modal de Detailed SIA. Durante la revisión identifiqué dos bugs independientes que combinados producían el comportamiento reportado:

- Inicié sesión con un usuario de prueba con roles IPT y accedí al detalle de un WSM para replicar el problema reportado.
- Observé que la página de detalle del WSM cargaba con errores de consola indicando 403 Forbidden en 5 endpoints de scoring del backend PointScore, lo que impedía que los datos del WSM se cargaran correctamente para usuarios IPT.
- Identifiqué que los 6 endpoints de cálculo de scores en el backend PointScore tenían una restricción de autorización que solo permitía los roles Admin, ProgramManager y ConfigurationManager, bloqueando a todos los roles IPT con un error 403.
- Corregí la restricción de autorización en los 6 endpoints para incluir todos los roles IPT y los roles WSMOwner y BlockOwner.
- Con los errores de 403 resueltos, intenté abrir el modal de Detailed SIA con el usuario IPT y comprobé que el modal aparecía vacío con el mensaje "No sections available for the impacted functional areas", a pesar de que el WSM tenía áreas impactadas asignadas que coincidían con los roles del usuario.
- Agregué logs de diagnóstico para inspeccionar en tiempo real los datos que llegaban al filtro del modal y confirmé que el endpoint devolvía los datos correctamente y que las áreas de la base de datos tenían exactamente los nombres esperados.
- Identifiqué que la lógica de filtrado del modal priorizaba el lookup del usuario en la lista de miembros IPT asignados al WSM, y cuando ese lookup fallaba o producía un resultado no coincidente, el modal quedaba vacío.
- Reestructuré la lógica para que el filtro use directamente los roles del usuario como método principal, con el lookup por ID como fallback y un safety net final que garantiza que el modal nunca aparezca vacío cuando hay datos disponibles.
- Verifiqué que tras ambas correcciones los usuarios IPT podían acceder al modal de Detailed SIA y ver las secciones correspondientes a sus áreas funcionales.

**Bugs encontrados y corregidos:** 2
- Bug 1: Endpoints de scoring con restricción de roles que bloqueaba a usuarios IPT con 403 Forbidden
- Bug 2: Modal Detailed SIA Assessment vacío para usuarios IPT por lógica de filtrado incorrecta
