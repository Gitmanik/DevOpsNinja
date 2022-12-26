kind create cluster --config kind-config.yaml

@REM Instalacja Contour
kubectl apply -f https://projectcontour.io/quickstart/contour.yaml
@REM Patch Contour do działania z Kind
kubectl patch daemonsets -n projectcontour envoy -p {"""spec""":{"""template""":{"""spec""":{"""nodeSelector""":{"""ingress-ready""":"""true"""},"""tolerations""":[{"""key""":"""node-role.kubernetes.io/control-plane""","""operator""":"""Equal""","""effect""":"""NoSchedule"""},{"""key""":"""node-role.kubernetes.io/master""","""operator""":"""Equal""","""effect""":"""NoSchedule"""}]}}}}

@REM Instalacja aplikacji
build