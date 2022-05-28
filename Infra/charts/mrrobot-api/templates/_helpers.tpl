{{/*
Expand the name of the chart.
*/}}
{{- define "mrrobot-api.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "mrrobot-api.fullname" -}}
{{- if .Values.fullnameOverride }}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- $name := default .Chart.Name .Values.nameOverride }}
{{- if contains $name .Release.Name }}
{{- .Release.Name | trunc 63 | trimSuffix "-" }}
{{- else }}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" }}
{{- end }}
{{- end }}
{{- end }}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "mrrobot-api.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" }}
{{- end }}

{{/*
Common labels
*/}}
{{- define "mrrobot-api.labels" -}}
helm.sh/chart: {{ include "mrrobot-api.chart" . }}
{{ include "mrrobot-api.selectorLabels" . }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end }}

{{/*
Selector labels
*/}}
{{- define "mrrobot-api.selectorLabels" -}}
app.kubernetes.io/name: {{ include "mrrobot-api.name" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- end }}

{{/*
Create the name of the service account to use
*/}}
{{- define "mrrobot-api.serviceAccountName" -}}
{{- if .Values.serviceAccount.create }}
{{- default (include "mrrobot-api.fullname" .) .Values.serviceAccount.name }}
{{- else }}
{{- default "default" .Values.serviceAccount.name }}
{{- end }}
{{- end }}

{{- define "kong.namespace" -}}
{{- default .Release.Namespace .Values.namespace -}}
{{- end -}}

{{- define "kong.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "kong.fullname" -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "kong.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{- define "kong.metaLabels" -}}
app.kubernetes.io/name: {{ template "kong.name" . }}
helm.sh/chart: {{ template "kong.chart" . }}
app.kubernetes.io/instance: "{{ .Release.Name }}"
app.kubernetes.io/managed-by: "{{ .Release.Service }}"
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- range $key, $value := .Values.extraLabels }}
{{ $key }}: {{ $value | quote }}
{{- end }}
{{- end -}}

{{- define "kong.selectorLabels" -}}
app.kubernetes.io/name: {{ template "kong.name" . }}
app.kubernetes.io/component: app
app.kubernetes.io/instance: "{{ .Release.Name }}"
{{- end -}}

{{/*
Create Ingress resource for a Kong service
*/}}
{{- define "kong.ingress" -}}
{{- $servicePort := include "kong.ingress.servicePort" . }}
{{- $path := .ingress.path -}}
{{- $hostname := .ingress.hostname -}}
{{- $pathType := .ingress.pathType -}}
apiVersion: {{ .ingressVersion }}
kind: Ingress
metadata:
  name: {{ .fullName }}-{{ .serviceName }}
  namespace: {{ .namespace }}
  labels:
  {{- .metaLabels | nindent 4 }}
  {{- if .ingress.annotations }}
  annotations:
    {{- range $key, $value := .ingress.annotations }}
    {{ $key }}: {{ $value | quote }}
    {{- end }}
  {{- end }}
spec:
{{- if (and (not (eq .ingressVersion "extensions/v1beta1")) .ingress.ingressClassName) }}
  ingressClassName: {{ .ingress.ingressClassName }}
{{- end }}
  rules:
  - host: {{ $hostname | quote }}
    http:
      paths:
        - backend:
          {{- if (not (eq .ingressVersion "networking.k8s.io/v1")) }}
            serviceName: {{ .fullName }}-{{ .serviceName }}
            servicePort: {{ $servicePort }}
          {{- else }}
            service:
              name: {{ .fullName }}-{{ .serviceName }}
              port:
                number: {{ $servicePort }}
            {{- end }}
          path: {{ $path }}
          {{- if (not (eq .ingressVersion "extensions/v1beta1")) }}
          pathType: {{ $pathType }}
          {{- end }}
  {{- if (hasKey .ingress "tls") }}
  tls:
  - hosts:
    - {{ $hostname | quote }}
    secretName: {{ .ingress.tls }}
  {{- end -}}
{{- end -}}

{{/*
Create Service resource for a Kong service
*/}}
{{- define "kong.service" -}}
apiVersion: v1
kind: Service
metadata:
  name: {{ .fullName }}-{{ .serviceName }}
  namespace: {{ .namespace }}
  {{- if .annotations }}
  annotations:
  {{- range $key, $value := .annotations }}
    {{ $key }}: {{ $value | quote }}
  {{- end }}
  {{- end }}
  labels:
    {{- .metaLabels | nindent 4 }}
  {{- range $key, $value := .labels }}
    {{ $key }}: {{ $value | quote }}
  {{- end }}
spec:
  type: {{ .type }}
  {{- if eq .type "LoadBalancer" }}
  {{- if .loadBalancerIP }}
  loadBalancerIP: {{ .loadBalancerIP }}
  {{- end }}
  {{- if .loadBalancerSourceRanges }}
  loadBalancerSourceRanges:
  {{- range $cidr := .loadBalancerSourceRanges }}
  - {{ $cidr }}
  {{- end }}
  {{- end }}
  {{- end }}
  {{- if .externalIPs }}
  externalIPs:
  {{- range $ip := .externalIPs }}
  - {{ $ip }}
  {{- end -}}
  {{- end }}
  ports:
  {{- if .http }}
  {{- if .http.enabled }}
  - name: kong-{{ .serviceName }}
    port: {{ .http.servicePort }}
    targetPort: {{ .http.containerPort }}
    appProtocol: http
  {{- if (and (or (eq .type "LoadBalancer") (eq .type "NodePort")) (not (empty .http.nodePort))) }}
    nodePort: {{ .http.nodePort }}
  {{- end }}
    protocol: TCP
  {{- end }}
  {{- end }}
  {{- if .tls.enabled }}
  - name: kong-{{ .serviceName }}-tls
    port: {{ .tls.servicePort }}
    targetPort: {{ .tls.overrideServiceTargetPort | default .tls.containerPort }}
    appProtocol: https
  {{- if (and (or (eq .type "LoadBalancer") (eq .type "NodePort")) (not (empty .tls.nodePort))) }}
    nodePort: {{ .tls.nodePort }}
  {{- end }}
    protocol: TCP
  {{- end }}
  {{- if (hasKey . "stream") }}
  {{- range .stream }}
  - name: stream{{ if (eq (default "TCP" .protocol) "UDP") }}udp{{ end }}-{{ .containerPort }}
    port: {{ .servicePort }}
    targetPort: {{ .containerPort }}
    {{- if (and (or (eq $.type "LoadBalancer") (eq $.type "NodePort")) (not (empty .nodePort))) }}
    nodePort: {{ .nodePort }}
    {{- end }}
    protocol: {{ .protocol | default "TCP" }}
  {{- end }}
  {{- end }}
  {{- if .externalTrafficPolicy }}
  externalTrafficPolicy: {{ .externalTrafficPolicy }}
  {{- end }}
  {{- if .clusterIP }}
  clusterIP: {{ .clusterIP }}
  {{- end }}
  selector:
    {{- .selectorLabels | nindent 4 }}
{{- end -}}

