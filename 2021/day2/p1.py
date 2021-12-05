hor = 0
depth = 0
with open('p1.in') as file:
    for line in file:
        l = line.strip().split()
        command = l[0].lower()
        dist = int(l[1])
        if(command == "forward"):
            hor += dist
        elif(command == "down"):
            depth += dist
        elif(command == "up"):
            depth -= dist

    if(depth < 0):
        print("oh my")

print(hor * depth)
        