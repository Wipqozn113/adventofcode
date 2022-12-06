with open('input.in') as file:
    for line in file:
        marker = []
        c = 0
        for l in line:  
            c += 1      
            marker.append(l)
            if len(marker) == 14:
                if len(marker) == len(set(marker)):
                    print(c)
                    break
                marker.pop(0)


