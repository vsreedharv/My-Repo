import re

String1 = "This is a Python program for string compare"
String2 = "Python"

if re.search(String2,String1):
    print("Yes, string {0} found in string {1}".format(String1,String2))
else:
    print("No, string {0} not found in string {1}".format(String1,String2))