{{- define "suffix-name" -}}
{{- if .Values.app.name -}}
{{- .Values.app.name -}}
{{- else -}}
{{- .Release.Name -}}
{{- end -}}
{{- end -}}

{{- define "sql-name" -}}
{{- if .Values.inf.sql.host -}}
{{- .Values.inf.sql.host -}}
{{- else -}}
{{- printf "%s" "sql-data" -}}
{{- end -}}
{{- end -}}

{{- define "url-of" -}}
{{- $name := first .}}
{{- $ctx := last .}}
{{- if eq $name "" -}}
{{- $ctx.Values.inf.k8s.dns -}}
{{- else -}}
{{- printf "%s/%s" $ctx.Values.inf.k8s.dns $name -}}                {{/*Value is just <dns>/<name> */}}
{{- end -}}
{{- end -}}

{{- define "pathBase" -}}
{{- $svcName := include "svc.name" . -}}
{{- if .Values.inf.k8s.suffix -}}
{{- $suffix := include "suffix-name" . -}}
{{- printf "%s-%s" $svcName $suffix -}}
{{- else -}}
{{- printf "/%s" $svcName -}}
{{- end -}}
{{- end -}}

{{- define "fqdn-image" -}}
{{- $suffix := include "suffix-name" . -}}
{{- $svcName := include "svc.name" . -}}
{{- if .Values.inf.registry -}}
{{- printf "%s/%s/%s" .Values.inf.registry.server $suffix $svcName -}}
{{- else -}}
{{- printf "%s/%s" $suffix $svcName -}}
{{- end -}}
{{- end -}}