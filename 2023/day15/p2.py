class Step:
    def __init__(self, step):
        self.hash_value = None
        self.step = step
        if "=" in step:
            stp = step.split("=")
            self.label = stp[0]
            self.focal_length = int(stp[1])
            self.operation = "="
        else:
            self.label = step[:-1]
            self.operation = "-"

    def calc_hash(self):
        if self.hash_value is not None:
            return self.hash_value
        
        '''
        Determine the ASCII code for the current character of the string.
        Increase the current value by the ASCII code you just determined.
        Set the current value to itself multiplied by 17.
        Set the current value to the remainder of dividing itself by 256.
        '''
        current_value = 0
        for char in self.label:
            current_value += ord(char)
            current_value *= 17
            current_value = current_value % 256

        self.hash_value = current_value

        return self.hash_value

class Boxes:
    def __init__(self):
        self.lenses = {}

    def add_lense(self, lense):
        self.lenses[lense.label] = lense
    
    def remove_lense(self, lense):
        if lense.label in self.lenses:
            del self.lenses[lense.label]

    def total_focal_length(self, index):
        focal_length = 0
        i = 1
        for key in self.lenses:
            focal_length += ((index + 1) * i * self.lenses[key].focal_length)
            i += 1
        return focal_length


boxes = {}
steps = []
filename = "input.in"
with open(filename) as  file:
    for line in file:
        line = line.split(",")
        for step in line:
            steps.append(Step(step))

for step in steps:
    box = step.calc_hash()
    if box not in boxes:
        boxes[box] = Boxes()
    if step.operation == "=":
        boxes[box].add_lense(step)
    else:
        boxes[box].remove_lense(step)

'''
One plus the box number of the lens in question.
The slot number of the lens within the box: 1 for the first lens, 2 for the second lens, and so on.
The focal length of the lens.
'''
total = 0
for key in boxes:
    box = boxes[key]
    total += box.total_focal_length(key)
print(total)

