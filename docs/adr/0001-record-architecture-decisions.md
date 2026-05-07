# ADR 0001: Record Architecture Decisions

## Status
Accepted

## Date
05-06-2026

## Context
This project will accumulate architectural and design decisions over time — choices about libraries, patterns, conventions, and trade-offs. Without a written record, the *reasoning* behind these decisions is lost as soon as the conversation that produced them ends. New contributors (including the original author returning to the project after months away) cannot reconstruct *why* a given approach was chosen, only *that* it was. This leads to two failure modes: revisiting settled decisions repeatedly, and reversing decisions without understanding the original reasoning.

This repository serves as the Application Under Test for the `customizable-crucible` test framework. While this repo is not itself a portfolio piece, the architectural choices made here directly shape what the framework can demonstrate — choice of message bus, choice of data access library, service boundaries, contract shapes. Documenting those choices preserves the reasoning that ties this AUT to the framework's testing surface.

## Decision
This project will use Architecture Decision Records (ADRs) to document significant architectural and design decisions. ADRs follow the format originated by Michael Nygard:

- **Title**: Short, descriptive, numbered sequentially
- **Status**: Proposed, Accepted, Deprecated, or Superseded
- **Date**: ISO 8601 format
- **Context**: The situation or forces driving the need for the decision
- **Decision**: What was decided
- **Consequences**: The resulting trade-offs, both positive and negative

ADRs are stored as markdown files in `docs/adr/` with filenames of the form `NNNN-kebab-case-title.md`. Once accepted, an ADR is immutable - superseding decisions are recorded as new ADRs that reference the original.

## Consequences

## Positive
- Decisions and their reasoning are preserved for future reference
- Onboarding new contributors becomes easier - the *why* is documented alongside the *what*
- Forcing the discipline of writing an ADR slows down decision making in a productive way: *if* a decision is hard to justify in writing, it probably needs more thought

## Negative
- Adds a small overhead to architectural changes
- Requires discipline to maintain; ADRs that lag behind the codebase become misleading

## References
- Michael Nygard, "Documenting Architectural Decisions" (2011): https://cognitect.com/blog/2011/11/15/documenting-architecture-decisions
- adr.github.io - community-maintained ADR resources