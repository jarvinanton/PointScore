# Bug 2 — El modal "Detailed SIA Assessment" aparece vacío para usuarios con roles IPT

## Cómo encontré el bug

Revisé el acceso de los usuarios IPT al modal de Detailed SIA Assessment dentro del flujo de la Fase 3 del workflow de un WSM. Durante la revisión identifiqué el siguiente bug:

**Título del bug:** El modal "Detailed SIA Assessment" mostraba el mensaje "No sections available for the impacted functional areas" para todos los usuarios con roles IPT, mientras que para usuarios Admin el modal mostraba correctamente las secciones.

**Descripción del bug:** Al abrir el modal de Detailed SIA con un usuario que tenía múltiples roles IPT como IPTLogistics, IPTProductionGFE, IPTSafety, IPTCyber e IPTAcquisition, el modal aparecía completamente vacío sin mostrar ninguna sección, a pesar de que el WSM tenía áreas funcionales impactadas asignadas (IDs 1, 2, 3 correspondientes a Logistics, Production GFE y Safety) que coincidían con los roles del usuario.

---

## Cómo lo resolví

- Agregué logs de diagnóstico en el código para inspeccionar los datos en tiempo de ejecución al abrir el modal con el usuario afectado.
- Confirmé que el endpoint devolvía correctamente 77 secciones distribuidas en 12 áreas funcionales.
- Confirmé que el filtro por áreas impactadas del WSM funcionaba y producía 3 áreas con sus secciones correspondientes.
- Confirmé que los nombres de las áreas en la base de datos eran exactamente `"Logistics"`, `"Production GFE"` y `"Safety"`, y que los roles del usuario eran `"IPTLogistics"`, `"IPTProductionGFE"` e `"IPTSafety"`.
- Identifiqué que la lógica de filtrado priorizaba buscar al usuario en la lista de miembros IPT asignados al WSM por su ID, y solo si no lo encontraba aplicaba el filtro por roles. Esta prioridad causaba que cuando el usuario sí estaba en la lista pero su área asignada no coincidía exactamente con el nombre del área filtrada, el resultado quedaba vacío.
- Identifiqué además que el bypass para Admin no incluía a los roles CM y PM, lo que también les impedía ver todas las secciones.
- Reestructuré la lógica para que el filtro por roles del usuario se ejecute primero como método principal, usando un mapa de roles a nombres de área con múltiples aliases para cubrir variaciones de nombres en la base de datos.
- Mantuve el lookup por ID de miembro IPT como fallback secundario en caso de que el filtro por roles no produzca resultados.
- Agregué un safety net final que muestra todas las áreas impactadas del WSM si todos los filtros anteriores devuelven vacío, garantizando que el modal nunca aparezca vacío cuando hay datos disponibles.
- Corregí el bypass para que incluya también a los roles CM y PM además de Admin y WSMOwner.
- Eliminé los logs de diagnóstico tras confirmar que el fix funcionaba correctamente.
- Verifiqué que el modal mostraba las secciones correctas para el usuario con roles IPT y que el Admin continuaba viendo todas las áreas sin cambios.
