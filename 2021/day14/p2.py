def Insert(template, rules, characters):
    temp = dict(template)
    add = {}
    remove = []
    for key, value in rules.items():
        if key in temp:
            char1, char2 = key
            count = temp[key]
            val1 = char1 + value
            val2 = value + char2
            if val1 not in add:
                add[val1] = 0
            if val2 not in add:
                add[val2] = 0

            add[val1] += count
            add[val2] += count

            if key not in remove:
                remove.append(key)

            IncrementChar(value, count, characters)
    for rem in remove:
        template[rem] = 0

    for key, value in add.items():
        InsertChar(key, value, template)
    
            
def IncrementChar(char, count, characters):
    if char not in characters:
        characters[char] = 0

    characters[char] += count  

def InsertChar(key, count, template):
    if key not in template:
        template[key] = 0

    template[key] += count
 
def FindMaxMin(chars):
    mx = max(chars.values())
    mn = min(chars.values())
    print(mx - mn)

template = {}
rules = {}
characters = {}
with open('p1.in') as file:
    for line in file:
        if line.strip() == "":
            continue

        l = line.strip().split("->")
        if len(l) == 2:
            rules[l[0].strip()] = l[1].strip()
        else:
            temp = l[0].strip()    
            for n in range(len(temp)):
                characters[temp[n]] = 0
                if n + 2 <= len(temp):
                    template[temp[n:n+2]] = 0        
            for n in range(len(temp)):
                characters[temp[n]] += 1
                if n + 2 <= len(temp):
                    template[temp[n:n+2]] += 1   

for n in range(40):
    Insert(template, rules, characters)

FindMaxMin(characters)

