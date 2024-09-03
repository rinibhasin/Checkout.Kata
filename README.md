# Checkout.Kata

A .NET solution implementing a checkout system as a coding kata.

## Table of Contents

- [Checkout.Kata](#checkoutkata)
  - [Features](#features)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Running Tests](#running-tests)

## Features

- Implements a supermarket checkout system
- Supports unit pricing and special/multiple pricing rules
- Developed using Test-Driven Development (TDD)

## Prerequisites

Before you begin, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) version 8.0 
- An IDE such as [Rider](https://www.jetbrains.com/rider/)

## Installation

To set up the `Checkout.Kata` project locally, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/rinibhasin/Checkout.Kata
    ```
2. Navigate to the project directory:
    ```bash
    cd Checkout.Kata
    ```
3. Restore the dependencies:
    ```bash
    dotnet restore
    ```

## Usage

To run the application locally:

1. Build the project:
    ```bash
    dotnet build
    ```
2. Run the project:
    ```bash
    dotnet run
    ```

## Running Tests

The project includes unit tests to validate the functionality of the checkout system.

To run the tests, use the following command:

1. Navigate to the tests directory:
    ```bash
    cd Checkout.Kata.Tests

```bash

dotnet test
