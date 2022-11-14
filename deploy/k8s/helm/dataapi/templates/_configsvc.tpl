{{/*
This template file is required to be configured for each service based on service key name in app.yaml
*/}}
{{- define "svc.name.lookup" -}}
{{- if .Values.app.svc.dataapi -}}
{{- .Values.app.svc.dataapi -}}
{{- else -}}
{{- .Chart.Name -}}
{{- end -}}
{{- end -}}

{{- define "svc.ingress.path.lookup" -}}
{{- if .Values.app.ingress.entries.dataapi -}}
{{- .Values.app.ingress.entries.dataapi -}}
{{- else -}}
{{- include "svc.name" . -}}
{{- end -}}
{{- end -}}