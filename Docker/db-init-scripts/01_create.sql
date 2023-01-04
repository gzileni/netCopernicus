GRANT ALL PRIVILEGES ON DATABASE copernicus TO copernicus;
CREATE SCHEMA IF NOT EXISTS gis;
CREATE SCHEMA IF NOT EXISTS dtm;
ALTER DATABASE copernicus SET search_path=public,gis,dtm;
GRANT ALL PRIVILEGES ON SCHEMA gis TO copernicus;
GRANT ALL PRIVILEGES ON SCHEMA dtm TO copernicus;

CREATE EXTENSION postgis;
CREATE EXTENSION postgis_raster;

ALTER DATABASE postgres SET postgis.enable_outdb_rasters = true;
ALTER DATABASE postgres SET postgis.gdal_enabled_drivers TO 'ENABLE_ALL';

ALTER DATABASE geo SET postgis.enable_outdb_rasters = true;
ALTER DATABASE geo SET postgis.gdal_enabled_drivers TO 'ENABLE_ALL';

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;
