# SANS-Newsletter-Get
 Get SANS Newsletter as JSON

## How to use
Build the project and run Ouch.exe, newsletter JSON will be saved in output directory in current working directory.  
Additional parameter: Ouch.exe <int> will force the JSON formatted with <int> spaces, if not given then minified JSON is generated.  

## Why?
[Cyber Security Company SANS, Packed ALL Newsletters in a 34MB JS File on website](https://attic.qinlili.bid/2025/04/cyber-security-company-sans-packed-all.html)  
I don't like the slow speed to load the website, so I wrote this tool to get the newsletter as JSON, and parse by my own.  

## Future plans
- Add another program to download the newsletter as PDF/EPUB for offline reading.  

## CI
You can get the latest newsletter JSON each day from GitHub Actions. [![Fetch SANS Newsletters](https://github.com/qinlili23333/SANS-Newsletter-Get/actions/workflows/fetch.yaml/badge.svg)](https://github.com/qinlili23333/SANS-Newsletter-Get/actions/workflows/fetch.yaml)  