# VoxSmart - Gareth Young

This repo is designed to perform the Exercise 2 section of the VoxSmart take-home test, which is to read an RSS feed and search for financial entities within that feed.

There is a simple RSS Reader class that obtains the source data.  There is additionally a set of classes designed to read information about financial entities from various CSV files.

These sets of data are then consumed in an extractor class, which enumerates the items from the RSS reader and returns any item that contains a financial entity name within it in a dictionary, with the values of the dictionary representing a collection of all the financial entities that were detected.

### Scope for improvements
- Rather than using a static CSV file to obtain the currency/commodity/company data, this could be read from a remote API (e.g. Morningstar) and cached. The current interface design is asynchronous to allow for this extension to happen in future without having to modify the consuming code.
- Calls to remote services either for the RSS feed or financial data could be protected via caching, wait/retry, circuit breaker and fallback policies.
- Searching is not currently performed on an entity's ticker/symbol - this could be implemented but thought needs to be given to the risk of it returning false positives (e.g. with simple ticker symbols such as `E`)
- Pattern matching of text could be improved - currently it relies on a simple `string.Contains` implementation, but this may not match similar but not-quite matching values and will match values where the input text is very short/simple but does not represent a financial entity.
- Validation of input data sources - values that are null, empty or too short to be useful should be excluded when reading data from any source.
- The current search algorithm is not very efficient - there are multiple enumerations of financial data sources.  These could be cached into memory and performed in parallel by moving away from nested foreach loops.