# Cómo acceder al SQL Server de la oficina desde otra computadora

## Idea clave (léelo dos veces)

**Internet** no hace que dos computadoras estén “en la misma red” solas por estar en línea. Para que tu app (Python o .NET) hable con SQL Server, hace falta **una ruta de red** donde el **puerto de SQL** (casi siempre **1433**, o otro si lo fijaron) sea alcanzable **desde tu PC hasta el servidor**.

El servidor “solo en la red del servidor” significa: **solo las PCs que están en esa misma capa de red (o detrás de un puente/VPN que las una) pueden verlo.**

---

## Caso 1: Estás físicamente en la oficina y el servidor es “otra red” extra

Ejemplo: el servidor está en `192.168.10.x` y el Wi‑Fi de invitados es `192.168.1.x` y **no hay ruta** entre ellos.

**Opciones (de más simple a más trabajo):**

1. **Conectar tu laptop también a la red del servidor**  
   - Cable Ethernet a ese switch/VLAN, o  
   - Wi‑Fi de la oficina que **sí** tenga acceso a esa subred (a veces hay SSID “Corp” vs “Invitados”).  
   Luego pruebas en terminal: `ping IP_DEL_SQL` y un test de puerto (1433 o el que usen).

2. **Que TI abra en el router/firewall una regla controlada**  
   Desde la subred donde estás tú **hacia** la IP del SQL Server y el puerto TCP del motor.  
   Ojo: eso **no** es “abrir SQL a internet”; es **rutear entre VLANs de la misma oficina** con reglas claras.

3. **Una PC “puente” con dos redes** (la que ya tiene acceso al servidor y a internet)  
   No suele ser la solución ideal para que *tú* te conectes: mejor VPN o unión de redes con TI.

---

## Caso 2: Estás en casa (o fuera) y el servidor sigue en la oficina

Aquí **no** comparten la misma LAN. Lo habitual y más seguro:

1. **VPN a la oficina** (la que use la empresa: FortiClient, Cisco, WireGuard interno, etc.)  
   Cuando la VPN conecta, tu Mac “aparece” dentro de la red de la oficina y puedes poner en `.env` la misma IP/host que usan allá.

2. **Red privada tipo Tailscale / ZeroTier** (solo si TI lo aprueba)  
   Instalan el agente en **un** servidor o bastión que vea SQL y en tu laptop; se crea una IP virtual privada. Sigue siendo “red privada”, no publicar SQL al mundo.

3. **Túnel SSH** (si tienen un Linux o Windows con SSH en la oficina que **sí** vea SQL)  
   Tu Mac hace `ssh -L 14330:IP_SQL_INTERNA:1433 usuario@servidor_bastion` y en la app conectas a `localhost,14330`.  
   Requiere usuario/claves y que TI lo permita.

**No recomendado sin TI muy involucrado:** abrir el puerto 1433 del SQL Server **directo a internet** con la IP pública del router. Eso recibe ataques constantes; si se hace, suele ir con **lista blanca de IPs**, VPN, o cambio de puerto + auditoría.

---

## Comprobar que “hay camino” antes de culpar a Python

En la computadora desde la que quieres conectar (Mac o Windows):

1. `ping IP_O_HOST_SQL`  
   Si no responde, el problema es **red/firewall/VPN**, no la app.

2. Probar puerto (ejemplo Mac con `nc` si está instalado):  
   `nc -vz IP_SQL 1433`  
   Si “succeeded”, la ruta TCP existe.

3. Instancia con nombre (`SERVIDOR\COMERCIAL`): a veces hace falta **puerto TCP fijo** en SQL Server o abrir **UDP 1434** (Browser). Si TI puede, que dejen **puerto fijo** y uses `SERVIDOR,puerto` en la cadena de conexión; suele dar menos dolores de cabeza.

---

## Resumen en una frase

Para “estar en internet y a la vez poder usar el servidor local”, lo normal es **VPN (o túnel/malla aprobada)** que te meta en la **misma red lógica** que el SQL, o que **tu laptop entre también** a la red extra donde vive el servidor. Internet solo no alcanza.

---

## Con quién hablar en la empresa

Una frase para TI: *“Necesito llegar por TCP desde mi equipo [ubicación] hasta el SQL Server `X` puerto `Y` para desarrollo; ¿VPN de oficina, regla entre VLANs, o bastión + túnel?”*
