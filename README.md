# Campaign Code and Receipt Parser

This is a C# console application that provides two main functionalities:
1. Secure, verifiable **campaign code generation and validation**.
2. Parsing of **receipt text** extracted from OCR output in JSON format.

---

## Project Modules

### 1. Campaign Code Generation

This module is responsible for generating unique, algorithmically verifiable campaign codes and validating them without the need for database storage.

#### CodeGenerator:

- **Purpose**: Generates secure, unpredictable 8 character campaign codes.
- **Structure**:
  - First 5 characters: Random characters selected from a custom character set (`ACDEFGHKLMNPRTXYZ234579`).
  - Last 3 characters: Checksum derived from HMAC-SHA256 of the first 5 characters using a secret key.
- **How it works**:
  The user is asked to provide information on how many codes they wants to generate. Generic codes are generated using the HMAC-SHA256 
  algorithm, which makes them reliable and difficult to predict.
- **Why HMAC-SHA256?**
  - Secret-based authentication: HMAC uses a secret key, so only trusted systems can generate valid codes.
  - Strong security: SHA-256 provides high entropy and resistance against collisions and forgery.
  - Security: The use of a secret key prevents forgery and makes codes difficult to reverse-engineer.
  
#### CodeValidator:

- **Purpose**: Validates if a given 8 character code is correctly structured and authenticated.
- **How it works**:
  - Splits the input into a 5 character random part and a 3 character checksum.
  - Recomputes the checksum and compares it with the provided one.
- **Benefit**: No need for database lookups validation is algorithm-based.

---

### 2. Receipt Parser

This module parses the Json text extracted from receipts to obtain meaningful output.

- **How it works**:
  - First, the corners of the frame are determined according to the coordinates specified in the json. The minimum X and maximum Y values ​​of the frame are determined. Then, I filter the descriptions of each item in the Deserialized json that are not null, have coordinate information, and are within our frame for each specified item coordinates, and I create an Ouput object instance for each. When the given json is examined, I see that there is a margin of 15 between the Y coordinates of each item to go to the next line. I return to the Output list that I have and print the line number and content of each item to the screen.

---

## Program.cs Console Interface

The main program offers three functionalities through a menu:

1. **Campaign Code Generation**  
   - Asks for the number of codes to generate.
   - Prints the generated secure campaign codes.

2. **Campaign Code Validation**  
   - Asks for a single code to validate.
   - Displays whether the code is valid or not.

3. **Parse Receipt Text from OCR JSON**  
   - Parses `ReceiptParser/response.json` and prints structured receipt lines.

---

## Security Design

- Code generation and validation rely on a **secret key**.
- The use of **HMAC-SHA256** ensures:
  - Code integrity
  - Prevention of unauthorized code generation

---

### Quick Start
```bash
# 1. Clone the repository
git clone https://github.com/sibeltere/campaign-code-and-receipt-parser.git
cd campaign-code-and-receipt-parser

# 2. Start
dotnet run
```
