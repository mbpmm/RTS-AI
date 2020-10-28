# RTS-AI

Proyecto para la materia de inteligencia artificial.

## Consigna

Realizar juego tipo RTS con 2 tipos de entidades (exploradores y mineros)

● El nivel puede ser 2D o 3D, debe tener obstáculos, y debe haber una base central (HQ)
para las entidades

● El pathfinding de ambas entidades será realizado con una implementación del algoritmo A*

● Debe haber un HUD que me indique la cantidad de oro que tengo.

● Por algún evento del usuario (teclado o mouse) se crean entidades, siempre que se tenga
oro suficiente, y se descuenta del total lo que valga la entidad creada.

● Un spawner de minas se ocupa de crear SPOTS a ser explorados, de donde vendrán las
minas luego. Debe mantener un límite (por ej, 3) de SPOTS aún no explorados.

● La entidad Explorador tiene una FSM con los siguientes estados:

  ○ Idle: Al crearse la instancia, permanece 5 segundos en este estado antes de pasar al
    siguiente
    
  ○ Patrol: elige una posición al azar dentro de un radio determinado, y se dirige hacia
    allí luego de haber obtenido un camino con A*.
    En este estado, el explorador busca los SPOTS que están en su cono de visión, y si
    detecta alguno, pasa al estado Marking
    
  ○ Marking: el explorador se dirige hacia el SPOT detectado, y al alcanzarlo lo
    transforma en una mina detectable por los mineros ( planta un banderín), luego de lo
    cual vuelve al estado Patrol
    
● La entidad Minero tiene una FSM con los siguientes estados

  ○ Idle: Al crearse la instancia, permanece 5 segundos en este estado antes de pasar al
    siguiente
    
  ○ Patrol: elige una posición al azar dentro de un radio determinado, y se dirige hacia
    allí luego de haber obtenido un camino con A*.
    En este estado, el minero busca las minas que hayan marcado los exploradores, y si
    detecta alguna que están en su cono de visión, pasa al estado Mining
    
  ○ Mining: el minero se dirige hacia la mina detectada, y extrae oro de la mina a cierta
    tasa (por ej, una unidad de oro por segundo) mientras la mina no se agote ni alcance
    su capacidad de carga (por ej, 10 unidades de oro). Luego pasa a Returning
    ○ Returning: el minero ha alcanzado su límite de carga, o bien la mina se ha agotado,
    y entonces se dirige al HQ a descargar, incrementando el oro total.
    Si la mina no se había agotado al pasar a este estado, vuelve al estado Mining; de
    otro modo, pasa al estado Patrol.
    
● Ambas entidades detectan un objeto en su cono de visión si:

  ○ Está dentro de cierto radio
  
  ○ El ángulo formado entre el forward de la entidad y la dirección al objeto es menor a
    cierta apertura
    
  ○ Un raycast hacia el objeto no detecta obstáculos
  
