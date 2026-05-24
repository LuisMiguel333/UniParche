#!/bin/bash
# Nombre del archivo de salida
OUTPUT="proyecto_completo.txt"

# Borrar el archivo si ya existe
rm -f "$OUTPUT"

# Buscar archivos, excluyendo directorios innecesarios
find . -type f \
  -not -path '*/.*' \
  -not -path '*/node_modules/*' \
  -not -path '*/dist/*' \
  -not -path '*/build/*' \
  -not -name "concatenar.sh" \
  -not -name "proyecto_completo.txt" \
  -not -name "*.log" \
  -not -name "*.DS_Store" | while read -r file; do
    echo "--- INICIO DEL ARCHIVO: $file ---" >> "$OUTPUT"
    cat "$file" >> "$OUTPUT"
    echo -e "\n\n--- FIN DEL ARCHIVO: $file ---\n" >> "$OUTPUT"
done
echo "¡Listo! Todo tu proyecto está en $OUTPUT"
